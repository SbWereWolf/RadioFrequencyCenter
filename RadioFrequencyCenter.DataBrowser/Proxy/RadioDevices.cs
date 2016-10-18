using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class RadioDevices
    {
        public class SelectionCriteria
        {
            public readonly RadioDevice PrimarySelectionCriteria;
            public readonly RadioDevice SecondarySelectionCriteria;
            public bool IsPrimaryIdResNull = true;
            public bool IsSecondaryIdResNull = true;
            public  bool LetUseSecondary { get; private set; }

            private const string PrimaryNumSvid = "NumSvid";
            private const string PrimaryZavNum = "ZavNum";
            private const string PrimaryLat = "Lat";
            private const string PrimaryLong = "Long";
            private const string PrimaryMac = "Mac";
            private const string PrimaryIds = "Ids";

            private const string PrimaryIdRes = "IdRes";
            private const string SecondaryIdRes = "IdRes1";
            private const string PrimaryRegion = "Region";
            private const string SecondaryRegion = "Region1";

            private const string PrimaryDateSvid = "DateSvid";
            private const string SecondaryDateSvid = "DateSvid1";
            private const string PrimarySrokSvid = "SrokSvid";
            private const string SecondarySrokSvid = "SrokSvid1";
            private const string PrimaryUpdateDate = "UpdateDate";
            private const string SecondaryUpdateDate = "UpdateDate1";
            private const string PrimaryDelDate = "DelDate";
            private const string SecondaryDelDate = "DelDate1";

            private const string LetUseSecondaryName = "searchWithRange";

            public SelectionCriteria()
            {
                PrimarySelectionCriteria = new RadioDevice();
                SecondarySelectionCriteria = new RadioDevice();
            }

            // ReSharper disable UnusedMethodReturnValue.Global
            public bool DefineCriteria(NameValueCollection requestForm)
                // ReSharper restore UnusedMethodReturnValue.Global
            {
                var result = false;
                if (requestForm != null
                    && PrimarySelectionCriteria != null
                    && SecondarySelectionCriteria != null)
                {
                    // Primary

                    PrimarySelectionCriteria.NumSvid = requestForm[PrimaryNumSvid];
                    DateTime datesvidPrimary;
                    var isSuccess = DateTime.TryParse(requestForm[PrimaryDateSvid], out datesvidPrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.DateSvid = datesvidPrimary;
                    }
                    DateTimeOffset deldatePrimary;
                    isSuccess = DateTimeOffset.TryParse(requestForm[PrimaryDelDate], out deldatePrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.DelDate = deldatePrimary;
                    }
                    long idresPrimary;
                    isSuccess = long.TryParse(requestForm[PrimaryIdRes], out idresPrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.IdRes = idresPrimary;
                        IsPrimaryIdResNull = false;
                    }
                    PrimarySelectionCriteria.Ids = requestForm[PrimaryIds];
                    PrimarySelectionCriteria.Lat = requestForm[PrimaryLat];
                    PrimarySelectionCriteria.Long = requestForm[PrimaryLong];
                    PrimarySelectionCriteria.Mac = requestForm[PrimaryMac];
                    long regionPrimary;
                    isSuccess = long.TryParse(requestForm[PrimaryRegion], out regionPrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.Region = regionPrimary;
                    }
                    DateTime sroksvidPrimary;
                    isSuccess = DateTime.TryParse(requestForm[PrimarySrokSvid], out sroksvidPrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.SrokSvid = sroksvidPrimary;
                    }
                    DateTimeOffset updatedatePrimary;
                    isSuccess = DateTimeOffset.TryParse(requestForm[PrimaryUpdateDate], out updatedatePrimary);
                    if (isSuccess)
                    {
                        PrimarySelectionCriteria.UpdateDate = updatedatePrimary;
                    }
                    PrimarySelectionCriteria.ZavNum = requestForm[PrimaryZavNum];

                    //Secondary
                    
                    DateTime datesvidSecondary;
                    isSuccess = DateTime.TryParse(requestForm[SecondaryDateSvid], out datesvidSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.DateSvid = datesvidSecondary;
                    }
                    DateTimeOffset deldateSecondary;
                    isSuccess = DateTimeOffset.TryParse(requestForm[SecondaryDelDate], out deldateSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.DelDate = deldateSecondary;
                    }
                    long idresSecondary;
                    isSuccess = long.TryParse(requestForm[SecondaryIdRes], out idresSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.IdRes = idresSecondary;
                        IsSecondaryIdResNull = false;
                    }
                    long regionSecondary;
                    isSuccess = long.TryParse(requestForm[SecondaryRegion], out regionSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.Region = regionSecondary;
                    }
                    DateTime sroksvidSecondary;
                    isSuccess = DateTime.TryParse(requestForm[SecondarySrokSvid], out sroksvidSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.SrokSvid = sroksvidSecondary;
                    }
                    DateTimeOffset updatedateSecondary;
                    isSuccess = DateTimeOffset.TryParse(requestForm[SecondaryUpdateDate], out updatedateSecondary);
                    if (isSuccess)
                    {
                        SecondarySelectionCriteria.UpdateDate = updatedateSecondary;
                    }

                    bool useSecondary;
                    isSuccess = bool.TryParse(requestForm[LetUseSecondaryName], out useSecondary);
                    if (isSuccess || requestForm[LetUseSecondaryName] == "on")
                    {
                        LetUseSecondary = true;
                    }

                    result = true;
                }

                return result;
            }
        }
        public BroadcastStationsRepository Repository { get; }

        public readonly SelectionCriteria Criteria;

        public RadioDevices()
        {
            var broadcastStationsRepository = new BroadcastStationsRepository();
            Repository = broadcastStationsRepository;
            Criteria = new SelectionCriteria();
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

        public List<RadioDevice> SelectRecords()
        {
            var allRecords = new List<RadioDevice>();
            if (Repository?.BroadcastStations != null)
            {
                IQueryable<BroadcastStations> records = Repository.BroadcastStations;

                if (Criteria != null)
                {
                    var primary = Criteria.PrimarySelectionCriteria;
                    var secondary = Criteria.SecondarySelectionCriteria;
                    var useSecondary = Criteria.LetUseSecondary;

                    records = SelectStringFields(primary, records);
                    records = SelectByPrimaryAndSecondaryCriteria(useSecondary, primary, secondary, records);
                    records = SelectBySecondaryCriteria(useSecondary, primary, records);

                }

                var selectedRecords = records?.ToArray();

                if (selectedRecords != null)
                    foreach (var repositoryRecord in selectedRecords)
                    {
                        RadioDevice record = null;
                        if (repositoryRecord != null)
                        {
                            record = new RadioDevice
                            {
                                DateSvid = repositoryRecord.DATE_SVID,
                                DelDate = repositoryRecord.DEL_DATE,
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

        private IQueryable<BroadcastStations> SelectBySecondaryCriteria(bool useSecondary, RadioDevice primary, IQueryable<BroadcastStations> records)
        {
            if (!useSecondary && primary != null && records != null)
            {
                if (primary.Region.HasValue)
                {
                    records = records.Where(x => x.REGION >= primary.Region);
                }
                if (Criteria!= null)
                {
                    if (!Criteria.IsPrimaryIdResNull)
                    {
                        records = records.Where(x => x.ID_RES >= primary.IdRes);
                    }
                }

                if (primary.UpdateDate.HasValue)
                {
                    records = records.Where(x => x.UPDATE_DATE >= primary.UpdateDate);
                }
                if (primary.DateSvid.HasValue)
                {
                    records = records.Where(x => x.DATE_SVID >= primary.DateSvid);
                }
                if (primary.DelDate.HasValue)
                {
                    records = records.Where(x => x.DEL_DATE >= primary.DelDate);
                }
                if (primary.SrokSvid.HasValue)
                {
                    records = records.Where(x => x.SROK_SVID >= primary.SrokSvid);
                }
            }
            return records;
        }

        private IQueryable<BroadcastStations> SelectByPrimaryAndSecondaryCriteria(bool useSecondary, RadioDevice primary, RadioDevice secondary,
            IQueryable<BroadcastStations> records)
        {
            if (useSecondary && primary != null && secondary != null && records != null )
            {
                if (primary.Region.HasValue && secondary.Region.HasValue)
                {
                    records = records.Where(x => x.REGION >= primary.Region && x.REGION <= secondary.Region);
                }
                else
                {
                    if (primary.Region.HasValue && !secondary.Region.HasValue)
                    {
                        records = records.Where(x => x.REGION >= primary.Region);
                    }
                    else
                    {
                        if (!primary.Region.HasValue && secondary.Region.HasValue)
                        {
                            records = records.Where(x => x.REGION <= secondary.Region);
                        }
                    }
                }

                if (Criteria!= null)
                {
                    if (!Criteria.IsPrimaryIdResNull && !Criteria.IsSecondaryIdResNull)
                    {
                        records = records.Where(x => x.ID_RES >= primary.IdRes && x.ID_RES <= secondary.IdRes);
                    }
                    else
                    {
                        if (!Criteria.IsPrimaryIdResNull && Criteria.IsSecondaryIdResNull)
                        {
                            records = records.Where(x => x.ID_RES >= primary.IdRes);
                        }
                        else
                        {
                            if (Criteria.IsPrimaryIdResNull && !Criteria.IsSecondaryIdResNull)
                            {
                                records = records.Where(x => x.ID_RES <= secondary.IdRes);
                            }
                        }
                    }
                }


                if (primary.UpdateDate.HasValue && secondary.UpdateDate.HasValue)
                {
                    records = records.Where(x => x.UPDATE_DATE >= primary.UpdateDate && x.UPDATE_DATE <= secondary.UpdateDate);
                }
                else
                {
                    if (primary.UpdateDate.HasValue && !secondary.UpdateDate.HasValue)
                    {
                        records = records.Where(x => x.UPDATE_DATE >= primary.UpdateDate);
                    }
                    else
                    {
                        if (!primary.UpdateDate.HasValue && secondary.UpdateDate.HasValue)
                        {
                            records = records.Where(x => x.UPDATE_DATE <= secondary.UpdateDate);
                        }
                    }
                }
                if (primary.DateSvid.HasValue && secondary.DateSvid.HasValue)
                {
                    records = records.Where(x => x.DATE_SVID >= primary.DateSvid && x.DATE_SVID <= secondary.DateSvid);
                }
                else
                {
                    if (primary.DateSvid.HasValue && !secondary.DateSvid.HasValue)
                    {
                        records = records.Where(x => x.DATE_SVID >= primary.DateSvid);
                    }
                    else
                    {
                        if (!primary.DateSvid.HasValue && secondary.DateSvid.HasValue)
                        {
                            records = records.Where(x => x.DATE_SVID <= secondary.DateSvid);
                        }
                    }
                }
                if (primary.DelDate.HasValue && secondary.DelDate.HasValue)
                {
                    records = records.Where(x => x.DEL_DATE >= primary.DelDate && x.DEL_DATE <= secondary.DelDate);
                }
                else
                {
                    if (primary.DelDate.HasValue && !secondary.DelDate.HasValue)
                    {
                        records = records.Where(x => x.DEL_DATE >= primary.DelDate);
                    }
                    else
                    {
                        if (!primary.DelDate.HasValue && secondary.DelDate.HasValue)
                        {
                            records = records.Where(x => x.DEL_DATE <= secondary.DelDate);
                        }
                    }
                }
                if (primary.SrokSvid.HasValue && secondary.SrokSvid.HasValue)
                {
                    records = records.Where(x => x.SROK_SVID >= primary.SrokSvid && x.SROK_SVID <= secondary.SrokSvid);
                }
                else
                {
                    if (primary.SrokSvid.HasValue && !secondary.SrokSvid.HasValue)
                    {
                        records = records.Where(x => x.SROK_SVID >= primary.SrokSvid);
                    }
                    else
                    {
                        if (!primary.SrokSvid.HasValue && secondary.SrokSvid.HasValue)
                        {
                            records = records.Where(x => x.SROK_SVID <= secondary.SrokSvid);
                        }
                    }
                }
            }
            return records;
        }

        private static IQueryable<BroadcastStations> SelectStringFields(RadioDevice primary, IQueryable<BroadcastStations> records)
        {
            if (primary != null && records != null)
            {
                if (!string.IsNullOrWhiteSpace(primary.NumSvid))
                {
                    records = records.Where(x => x.NUM_SVID.Contains(primary.NumSvid));
                }
                if (!string.IsNullOrWhiteSpace(primary.Lat))
                {
                    records = records.Where(x => x.LAT.Contains(primary.Lat));
                }
                if (!string.IsNullOrWhiteSpace(primary.Long))
                {
                    records = records.Where(x => x.LONG.Contains(primary.Long));
                }
                if (!string.IsNullOrWhiteSpace(primary.Mac))
                {
                    records = records.Where(x => x.MAC.Contains(primary.Mac));
                }
                if (!string.IsNullOrWhiteSpace(primary.ZavNum))
                {
                    records = records.Where(x => x.ZAV_NUM.Contains(primary.ZavNum));
                }
                if (!string.IsNullOrWhiteSpace(primary.Ids))
                {
                    records = records.Where(x => x.IDS.Contains(primary.Ids));
                }
            }
            return records;
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

        public DateTimeOffset? GetLastUpdateDate()
        {
            var result = Repository?.BroadcastStations?.Max(r => r.UPDATE_DATE);
            return result;
        }
    }
}