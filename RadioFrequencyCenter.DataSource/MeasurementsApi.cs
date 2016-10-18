using System;
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

        public RadioStation[] GetRadioStationsAndSignals(SelectionCriteria selectionCriteria)
        {
            List<RadioStation> devicesRecords = null;

            var isEmptyUpdatedate = true;
            var isEmptyNumsvid = true;
            var isEmptyZavnum = true;

            var isEmptyCriteria = true;

            var stationCriteria = selectionCriteria?.Station;
            var isEmptyCriteriaObject = stationCriteria == null;

            DateTime? updatedate = null;
            var numsvid = string.Empty;
            int? zavnum = null;

            if (!isEmptyCriteriaObject)
            {
                updatedate = stationCriteria.UpdateDate;
                numsvid = stationCriteria.CertificateNumber;
                zavnum = stationCriteria.FactoryNumber;

                isEmptyUpdatedate = updatedate == null;
                isEmptyNumsvid = numsvid == null;
                isEmptyZavnum = zavnum == null;

                isEmptyCriteria = isEmptyUpdatedate && isEmptyNumsvid && isEmptyZavnum;
            }

            var updatedateCriteria = string.Empty;
            var numsvidCriteria = string.Empty;
            var zavnumCriteria = string.Empty;
            if (!isEmptyCriteria)
            {
                if (!isEmptyUpdatedate)
                {
                    updatedateCriteria = $" updateDate > CONVERT ( datetimeoffset , '{updatedate}'   , 0  )";
                }
                if (!isEmptyNumsvid)
                {
                    numsvidCriteria = $" certificateNumber LIKE '{numsvid}'";
                }
                if (!isEmptyZavnum)
                {
                    zavnumCriteria = $" factoryNumber = {zavnum}";
                }
            }
            var queryCriteria = new []{updatedateCriteria, numsvidCriteria, zavnumCriteria };

            var devicesQuery = @"
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
";
            if (isEmptyCriteria)
            {
                devicesQuery = $@"
{devicesQuery}
;
";
            }
            else
            {
                devicesQuery = $@"
{devicesQuery}
WHERE
";
                var letAddAndKeyword = false;
                foreach (var criteria in queryCriteria)
                {
                    if (!string.IsNullOrWhiteSpace(criteria))
                    {
                        devicesQuery = letAddAndKeyword 
                            ? $@"{devicesQuery} AND {criteria}" 
                            : $@"{devicesQuery} {criteria}";
                        letAddAndKeyword = true;
                    }
                }
                devicesQuery = $@"{devicesQuery};";

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
                getDataCommand.CommandText = devicesQuery;
            }

            SqlDataReader devicesQueryResults = null;
            if (getDataCommand != null)
            {
                try
                {
                    devicesQueryResults = getDataCommand.ExecuteReader();
                }
                catch (Exception)
                {
                    devicesQueryResults = null;
                }
            }
            
            if (devicesQueryResults != null )
            {
                devicesRecords = MapDataToList<RadioStation>(devicesQueryResults);
                devicesQueryResults.Close();
            }

            RadioStation[] stations = null;
            if (devicesRecords != null)
            {
                stations = devicesRecords.ToArray();
            }
            

            if (stations != null)
            {
                var firstIndex = stations.GetLowerBound(0);
                var lastIndex = stations.GetUpperBound(0);

                for (var index = firstIndex; index <= lastIndex; index++)
                {
                    if (stations[index] == null) continue;
                    var parentKey = stations[index].Guid;
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
                        stations[index].SignalsFrequencies = frequencies;
                    }
                }

            }

            if (dbConnection?.State != ConnectionState.Closed)
            {
                dbConnection?.Close();
            }

            return stations;
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
