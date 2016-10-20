using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using RadioFrequencyCenter.DataBrowser.Proxy;

namespace RadioFrequencyCenter.DataBrowser.Models
{
    public class RadioDevice
    {
        public long IdRes { get; set; }
        public string ZavNum { get; set; }
        public string NumSvid { get; set; }
        public DateTime? DateSvid { get; set; }
        public DateTime? SrokSvid { get; set; }
        public double? Region { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Ids { get; set; }
        public string Mac { get; set; }
        public DateTimeOffset? DelDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DeviceSignal[] RadioSignals { get; set; }
        public Guid? Guid { get; set; }

        public class DeviceSignal
        {
            public long IdF { get; set; }
            public long Res { get; set; }
            public double? Tn { get; set; }
            public double? Rn { get; set; }
            public Guid? Guid { get; set; }

            //public static List<DeviceSignal> GetFrequencyRecords(BroadcastStations instance)
            //{
            //    var frequencyRecords = new List<DeviceSignal>();

            //    var broadcastFrequencies = instance?.StationFrequencies;
            //    if (instance != null && broadcastFrequencies!= null)
            //    {
            //        var repositoryData = broadcastFrequencies.ToArray();

            //        foreach (var repositoryRecord in repositoryData)
            //        {
            //            if (repositoryRecord != null)
            //            {
            //                var record = new DeviceSignal
            //                {
            //                    IdF = repositoryRecord.ID_F,
            //                    Res= repositoryRecord.RES,
            //                    Rn= repositoryRecord.RN,
            //                    Tn= repositoryRecord.TN
            //                };
            //                frequencyRecords.Add(record);
            //            }
            //        }
            //    }
            //    return frequencyRecords;
            //}
        }

        public bool Boot(string recordId )
        {
            var result = false;
            long id;
            var isIdDefined = long.TryParse(recordId, out id);
            if (isIdDefined)
            {
                var proxy = new RadioDevices();
                var record = proxy.GetRecord(id);
                if (record != null)
                {
                    RadioSignals = record.RadioSignals;
                    DateSvid = record.DateSvid;
                    DelDate= record.DelDate;
                    IdRes= record.IdRes;
                    Ids= record.Ids;
                    Lat= record.Lat;
                    Long= record.Long;
                    Mac= record.Mac;
                    NumSvid= record.NumSvid;
                    Region= record.Region;
                    RadioSignals= record.RadioSignals;
                    SrokSvid= record.SrokSvid;
                    UpdateDate= record.UpdateDate;
                    ZavNum= record.ZavNum;

                    result = true;
                }
            }
            
            return result;
        }
        
        public static bool DeleteAllRecords()
        {
            var proxy = new RadioDevices();
            var result = proxy.DeleteAll();

            return result;
        }

        public static List<RadioDevice> SearchRadioDevice(NameValueCollection requestForm)
        {
            RadioDevices radioDevices = null;
            if (requestForm != null)
            {
                radioDevices = new RadioDevices();
                radioDevices.Criteria?.DefineCriteria(requestForm);
            }

            List<RadioDevice> allRecords = null;
            if (radioDevices != null)
            {
                allRecords = radioDevices.FetchRecords();
            }
            return allRecords;
        }
    }
}