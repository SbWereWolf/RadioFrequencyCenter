using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class RadioDevices
    {
        public BroadcastStationsRepository Repository { get; }

        public RadioDevices()
        {
            var broadcastStationsRepository = new BroadcastStationsRepository();
            Repository = broadcastStationsRepository;
        }

        public bool DeleteAll()
        {
            var result = false;

            var broadcastStations  = Repository?.BroadcastStations;
            var stationsContext = broadcastStations?.Context;

            var frequencies = new SignalFrequencies();
            var broadcastFrequencies = frequencies.Repository?.BroadcastFrequencies;
            var frequenciesContext = broadcastFrequencies?.Context;

            if (stationsContext != null && frequenciesContext != null )
            {
                broadcastFrequencies.DeleteAllOnSubmit(broadcastFrequencies.ToArray());
                broadcastStations.DeleteAllOnSubmit(broadcastStations.ToArray());

                try
                {
                    
                    frequenciesContext.SubmitChanges();
                    stationsContext.SubmitChanges();
                    result = true;
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return result;
        }

        public List<RadioDevice> GetAllRecords()
        {
            var allRecords = new List<RadioDevice>();
            if (Repository?.BroadcastStations != null)
            {
                foreach (var repositoryRecord in Repository.BroadcastStations)
                {
                    RadioDevice record = null;
                    if (repositoryRecord != null)
                    {
                        record = new RadioDevice
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
                    }

                    if (record != null)
                    {
                        allRecords.Add(record);
                    }
                }
            }
            return allRecords;
        }

        public RadioDevice GetRecord(long id)
        {   
            RadioDevice device = null;

            BroadcastStations stationRecord = null;
            var repositoryData = Repository?.BroadcastStations?.Where(r => r.ID_RES == id).ToArray();
            if (repositoryData?.Length > 0)
            {
                const long firstIndex = 0;
                stationRecord = repositoryData[firstIndex];
            }

            if (stationRecord != null)
            {
                device = new RadioDevice
                {
                    DateSvid = stationRecord.DATE_SVID,
                    DelDate = stationRecord.DATE_SVID,
                    IdRes = stationRecord.ID_RES,
                    Ids = stationRecord.IDS,
                    Lat = stationRecord.LAT,
                    Long = stationRecord.LONG,
                    Mac = stationRecord.MAC,
                    NumSvid = stationRecord.NUM_SVID,
                    Region = stationRecord.REGION,
                    SrokSvid = stationRecord.SROK_SVID,
                    UpdateDate = stationRecord.UPDATE_DATE,
                    ZavNum = stationRecord.ZAV_NUM
                };
            }
            var signals = new List<RadioDevice.SignalFrequency>();
            if (stationRecord?.BroadcastFrequencies != null)
            {
                var frequenciesRecords = stationRecord.BroadcastFrequencies;
                foreach (var frequencyRecord in frequenciesRecords)
                {
                    if (frequencyRecord != null)
                    {
                        var signal = new RadioDevice.SignalFrequency
                        {
                            Res = frequencyRecord.RES,
                            IdF = frequencyRecord.ID_F,
                            Rn = frequencyRecord.RN,
                            Tn = frequencyRecord.TN
                        };
                        signals.Add(signal);
                    }
                }
            }
            if (device != null)
            {
                device.RadioSignals = signals.ToArray();
            }
            return device;
        }


        public bool InsertStations(List<RadioDevice> radioDevices)
        {
            var result = false;
            
            var broadcastStationsRepository = Repository;
            if (broadcastStationsRepository != null)
            {
                var broadcastStations = RadioDevicesToBroadcastStations(radioDevices);
                var broadcastStationsArray = broadcastStations?.ToArray();

                if (broadcastStationsArray?.Length > 0)
                {
                    result = broadcastStationsRepository.InsertStations(broadcastStationsArray);
                }
            }
            return result;
        }

        private static EntitySet<BroadcastStations> RadioDevicesToBroadcastStations(List<RadioDevice> radioDevices)
        {
            var broadcastStations = new EntitySet<BroadcastStations>();
            if (radioDevices != null)
            {
                foreach (var device in radioDevices)
                {
                    if (device != null)
                    {
                        var station = new BroadcastStations
                        {
                            DATE_SVID = device.DateSvid,
                            DEL_DATE = device.DelDate,
                            IDS = device.Ids,
                            ID_RES = device.IdRes,
                            LAT = device.Lat,
                            LONG = device.Long,
                            MAC = device.Mac,
                            NUM_SVID = device.NumSvid,
                            REGION = device.Region,
                            SROK_SVID = device.SrokSvid,
                            UPDATE_DATE = device.UpdateDate,
                            ZAV_NUM = device.ZavNum
                        };

                        var radioSignals = device.RadioSignals;
                        var length = radioSignals?.Length;
                        var broadcastFrequencies = new EntitySet<BroadcastFrequencies>();
                        if (length > 0)
                        {
                            foreach (var signal in radioSignals)
                            {
                                if (signal != null)
                                {
                                    var frequrency = new BroadcastFrequencies
                                    {
                                        RN = signal.Rn,
                                        TN = signal.Tn
                                    };
                                    broadcastFrequencies.Add(frequrency);
                                }
                            }
                        }

                        station.BroadcastFrequencies = broadcastFrequencies;

                        broadcastStations.Add(station);
                    }
                }
            }
            return broadcastStations;
        }
    }
}