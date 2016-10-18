﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace RadioFrequencyCenter.DataSource
{
    public class MeasurementsApi : IMeasurementsApi
    {
        public int IsApiOnline()
        {
            return 1;
        }

        public RadioDevice[] GetRadioDevicesData(SelectionCriteria selectionCriteria)
        {
            List<RadioDevice> devisesRecords = null;

            var isEmptyFromDate = true;
            var isEmptyTillDate = true;

            var isEmptyCriteria = true;
            var isFullCriteria = false;

            var isEmptyCriteriaObject = selectionCriteria == null;

            DateTime? dateFrom = null;
            DateTime? dateTill = null;

            if (!isEmptyCriteriaObject)
            {
                dateFrom = selectionCriteria.DateFrom;
                dateTill = selectionCriteria.DateTill;

                isEmptyFromDate = dateFrom == null;
                isEmptyTillDate = dateTill == null;

                isEmptyCriteria = isEmptyTillDate && isEmptyFromDate ;
                isFullCriteria = !isEmptyTillDate && !isEmptyFromDate;
            }

            var updatedateCriteria = string.Empty;
            if (!isEmptyCriteria)
            {
                if ( !isEmptyFromDate )
                {
                    updatedateCriteria = $"> {dateFrom}";
                }
                if (!isEmptyTillDate)
                {
                    updatedateCriteria = $"< {dateTill}";
                }
                if (isFullCriteria)
                {
                    updatedateCriteria = $"BETWEEN {dateFrom} AND {dateTill}";
                }
            }

            var devisesQuery = @"
SELECT 
      [GUID] AS Guid
    , [factoryNumber] AS FactoryNumber
    , [certificateNumber] AS CertificateNumber
    , [certificateIssueDate] AS CertificateIssueDate
    , [certificateValidDate] AS CertificateValidDate
    , [SP_REGION_GAI] AS SpRegionGai
    , [locationLattitude] AS LocationLattitude
    , [locationLongitude] AS LocationLongitude
    , [LAC] AS Lac
    , [CI] AS Ci
    , [BSID] AS Bsid
    , [Mac] AS Mac
    , [isDeleted] AS IsDeleted
    , [DelDate] AS DelDate
    , [updateDate] AS UpdateDate
FROM
    [RESDB].[dbo].[RES]
;
";
            if (!string.IsNullOrEmpty(updatedateCriteria))
            {
                devisesQuery = $@"
{devisesQuery}
WHERE
    [updateDate] {updatedateCriteria}
;
";
            }

            var connectionsStrings = ConfigurationManager.ConnectionStrings;
            var dbConnectionString = string.Empty;
            const string connectionName = "FORGE-JITA";
            var connectionStringSetting = connectionsStrings?[connectionName];
            if (connectionStringSetting != null)
            {
                dbConnectionString = connectionStringSetting.ConnectionString;
            }

            SqlConnection dbConnection = null;
            if (!string.IsNullOrEmpty(dbConnectionString))
            {
                dbConnection = new SqlConnection(dbConnectionString);
            }

            try
            {
                dbConnection?.Open();
            }
            catch (Exception)
            {

                // throw;
            }

            SqlCommand getDataCommand = null;
            if ( dbConnection?.State == ConnectionState.Open )
            {
                getDataCommand = dbConnection.CreateCommand();
                getDataCommand.CommandText = devisesQuery;
            }

            SqlDataReader devisesQueryResults = null;
            if (getDataCommand != null)
            {
                try
                {
                    devisesQueryResults = getDataCommand.ExecuteReader();
                }
                catch (Exception)
                {
                    devisesQueryResults = null;
                }
            }
            
            if (devisesQueryResults != null )
            {
                devisesRecords = MapDataToList<RadioDevice>(devisesQueryResults);
                devisesQueryResults.Close();
            }

            RadioDevice[] devices = null;
            if (devisesRecords != null)
            {
                devices = devisesRecords.ToArray();
            }
            

            if (devices != null)
            {
                var firstIndex = devices.GetLowerBound(0);
                var lastIndex = devices.GetUpperBound(0);

                for (var index = firstIndex; index <= lastIndex; index++)
                {
                    if (devices[index] == null) continue;
                    var parentKey = devices[index].Guid;
                    var signalFrequenciesQueryText =
                        $@"
SELECT 
	  [GUID] AS Guid
	, [RES] AS Res
	, [TN] AS Tn
	, [RN] AS Rn
FROM 
	[RESDB].[dbo].[FRQ]
WHERE
    RES = '{parentKey}'
;
";
                    getDataCommand.CommandText = signalFrequenciesQueryText;
                    SqlDataReader frequenciesQueryResults;
                    try
                    {
                        frequenciesQueryResults = getDataCommand.ExecuteReader();
                    }
                    catch (Exception)
                    {
                        frequenciesQueryResults = null;
                    }
                    List<SignalFrequency> frequenciesRecords = null;
                    if (frequenciesQueryResults != null)
                    {
                        frequenciesRecords = MapDataToList<SignalFrequency>(frequenciesQueryResults);
                        frequenciesQueryResults.Close();
                    }

                    SignalFrequency[] frequencies = null;
                    if (frequenciesRecords != null)
                    {
                        frequencies = frequenciesRecords.ToArray();
                    }

                    if ( frequencies != null )
                    {
                        devices[index].SignalsFrequencies = frequencies;
                    }
                }

            }

            if (dbConnection?.State != ConnectionState.Closed)
            {
                dbConnection?.Close();
            }

            return devices;
        }

        private static List<T> MapDataToList<T>
        (IDataReader dr)
           where T : new()
        {
            var entitys = new List<T>();
            if (dr == null) return entitys;

            var hashtable = new Hashtable();
            var businessEntityType = typeof(T);
            var properties = businessEntityType.GetProperties();
            foreach (var info in properties.Where(info => info != null))
            {
                hashtable[info.Name] = info;
            }

            while (dr.Read())
            {
                var fieldCount = dr.FieldCount;
                var newObject = new T();
                for (var index = 0; index < fieldCount; index++)
                {
                    var fieldName = dr.GetName(index);
                    var fieldValue = dr.GetValue(index);
                    if (fieldName == null || fieldValue == null ) continue;
                    var info = (PropertyInfo)
                        hashtable[fieldName];
                    if (info == null) continue;
                    if (!info.CanWrite) continue;
                    
                    try
                    {
                        info.SetValue(newObject, fieldValue, null);
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
                entitys.Add(newObject);
            }

            return entitys;
        }

    }
}