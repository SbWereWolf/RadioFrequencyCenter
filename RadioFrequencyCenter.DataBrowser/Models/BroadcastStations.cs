using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using RadioFrequencyCenter.DataBrowser.MeasurementsApiService;
using RadioFrequencyCenter.DataBrowser.Proxy;

namespace RadioFrequencyCenter.DataBrowser.Models
{
    public class BroadcastStation
    {
        public long IdRes { get; private set; }

        public string ZavNum { get; private set; }

        public string NumSvid { get; private set; }

        public DateTime? DateSvid { get; private set; }

        public DateTime? SrokSvid { get; private set; }

        public double? Region { get; private set; }

        public string Lat { get; private set; }

        public string Long { get; private set; }

        public string Ids { get; private set; }

        public string Mac { get; private set; }

        public DateTimeOffset? DelDate { get; private set; }

        public DateTimeOffset? UpdateDate { get; private set; }

        public BroadcastFrequency[] Frequencies { get; set; }

        public static List<BroadcastStation> GetAllRecords()
        {
            var repository = new BroadcastStations();
            var repositoryData = repository.GetData();

            var allRecords = new List<BroadcastStation>();
            if (repositoryData != null)
            {
                foreach (var repositoryRecord in repositoryData)
                {
                    if (repositoryRecord != null)
                    {
                        var record = new BroadcastStation
                        {
                            DateSvid = repositoryRecord.DATE_SVID,
                            DelDate = repositoryRecord.DATE_SVID,
                            IdRes = repositoryRecord.ID_RES,
                            Ids = repositoryRecord.IDS,
                            Lat = repositoryRecord.LAT,
                            Long = repositoryRecord.LONG,
                            Mac = repositoryRecord.MAC,
                            NumSvid = repositoryRecord.NUM_SVID,
                            Region = repositoryRecord.REGION,
                            SrokSvid = repositoryRecord.SROK_SVID,
                            UpdateDate = repositoryRecord.UPDATE_DATE,
                            ZavNum = repositoryRecord.ZAV_NUM
                        };
                        allRecords.Add(record);
                    }
                }
            }
            return allRecords;
        }

        public static BroadcastStation GetRecord(string recordId )
        {
            long id;
            var isIdDefined = long.TryParse(recordId, out id);
            BroadcastStation record = null;
            
            DataAccessLayer.BroadcastStations station = null;
            if (isIdDefined)
            {
                var repository = new BroadcastStations();
                var repositoryData = repository.Repository?.BroadcastStations?.Where(r => r.ID_RES == id).ToArray();
                if (repositoryData?.Length > 0)
                {
                    const long firstIndex = 0;
                    station = repositoryData[firstIndex];
                }
            }

            if (station != null)
            {
                record = new BroadcastStation
                {
                    DateSvid = station.DATE_SVID,
                    DelDate = station.DATE_SVID,
                    IdRes = station.ID_RES,
                    Ids = station.IDS,
                    Lat = station.LAT,
                    Long = station.LONG,
                    Mac = station.MAC,
                    NumSvid = station.NUM_SVID,
                    Region = station.REGION,
                    SrokSvid = station.SROK_SVID,
                    UpdateDate = station.UPDATE_DATE,
                    ZavNum = station.ZAV_NUM,
                    //Frequencies = frequencies
                };
            }
            List<BroadcastFrequency> frequencies = new List<BroadcastFrequency>();
            if (station?.BroadcastFrequencies != null)
            {
                List<DataAccessLayer.BroadcastFrequencies> frequenciesRecords = station.BroadcastFrequencies.ToList();
                foreach (var frequencyRecord in frequenciesRecords)
                {
                    if (frequencyRecord != null)
                    {
                        var broadcastFrequency = new BroadcastFrequency
                        {
                            Res = frequencyRecord.RES,
                            IdF = frequencyRecord.ID_F,
                            Rn = frequencyRecord.RN,
                            Tn = frequencyRecord.TN
                        };
                        frequencies.Add(broadcastFrequency);
                    }
                }
            }
            if (record != null)
            {
                record.Frequencies = frequencies.ToArray();
            }
            return record;
        }

        public static bool DownloadAllRecords()
        {
            var result = false;
            var apiClient = new MeasurementsApiClient();
            var rawRecords = apiClient.GetElectronicDevicesData(null);

            BroadcastStations proxy = null;
            if (rawRecords != null)
            {
                proxy = new BroadcastStations();
            }
            if (proxy?.Repository != null)
            {
                foreach (var rawRecord in rawRecords)
                {
                    if (rawRecord != null)
                    {
                        var instance = new DataAccessLayer.BroadcastStations
                        {
                            DATE_SVID = rawRecord.CertificateIssueDate,
                            DEL_DATE = rawRecord.DelDate,
                            IDS = $" Bsid : {rawRecord.Bsid} ; Ci : {rawRecord.Ci} ; Lac : {rawRecord.Lac} ; ",
                            LAT = rawRecord.LocationLattitude,
                            LONG = rawRecord.LocationLongitude,
                            MAC = rawRecord.Mac,
                            NUM_SVID = rawRecord.CertificateNumber,
                            REGION = rawRecord.SpRegionGai,
                            SROK_SVID= rawRecord.CertificateValidDate,
                            UPDATE_DATE = rawRecord.UpdateDate,
                            ZAV_NUM = rawRecord.FactoryNumber.ToStringInvariant()
                        };

                        var isSuccess = proxy.Repository.InsertStation(instance);
                        if (isSuccess)
                        {
                            result = true;
                        }
                    }

                }
            }

            return result;
        }
        public static bool DeleteAllRecords()
        {
            var proxy = new BroadcastStations();
            bool result = proxy.DeleteAll();

            return result;
        }
    }
}