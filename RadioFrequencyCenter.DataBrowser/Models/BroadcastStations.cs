using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
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

        public static List<BroadcastStation> GetAllRecords()
        {
            var repository = new Proxy.BroadcastStations();
            var repositoryData = repository.GetData();

            var allRecords = new List<Models.BroadcastStation>();
            if (repositoryData != null)
            {
                foreach (var repositoryRecord in repositoryData)
                {
                    if (repositoryRecord != null)
                    {
                        var record = new Models.BroadcastStation
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

        public static bool DownloadAllRecords()
        {
            var result = false;
            var apiClient = new MeasurementsApiService.MeasurementsApiClient();
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
                            IDS = $" Bsid : {rawRecord.Bsid} + Ci : {rawRecord.Ci} + Lac : {rawRecord.Lac}",
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