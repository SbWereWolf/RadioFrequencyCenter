﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class BroadcastStationsRepository : Repository
    {
        public BroadcastStationsRepository ()
        {
            BroadcastStations = RepositoryData?.BroadcastStations;
        }

        public Table<BroadcastStations> BroadcastStations { get; }

        public bool InsertStation(BroadcastStations instance)
        {
            var result = false;
            var stations = BroadcastStations;
            var context = stations?.Context;

            if (instance != null && context != null)
            {
                stations.InsertOnSubmit(instance);

                try
                {
                    context.SubmitChanges();
                    result = true;
                }
                catch (Exception)
                {

                    // throw;
                }
            }

            return result;
        }

        //public bool InsertLinkToResdbRes(IEnumerable<BroadcastStations> stations)
        //{
        //    var result = false;
        //    var broadcastStations = BroadcastStations;
        //    var context = broadcastStations?.Context;

        //    if (broadcastStations != null && context != null && stations != null)
        //    {
        //        broadcastStations.InsertAllOnSubmit(stations);

        //        try
        //        {
        //            context.SubmitChanges();
        //            result = true;
        //        }
        //        catch (Exception)
        //        {

        //            // throw;
        //        }
        //    }

        //    return result;
        //}

        public bool UpdateStation(BroadcastStations instance)
        {
            var result = false;
            var stations = BroadcastStations;
            var context = stations?.Context;
            if (instance != null && context != null)
            {
                var cache = stations.FirstOrDefault(p => p.ID_RES == instance.ID_RES);
                if (cache != null)
                {
                    cache.DATE_SVID = instance.DATE_SVID;
                    cache.DEL_DATE = instance.DEL_DATE;
                    cache.IDS = instance.IDS;
                    cache.LAT = instance.LAT;
                    cache.LONG = instance.LONG;
                    cache.MAC = instance.MAC;
                    cache.NUM_SVID = instance.NUM_SVID;
                    cache.REGION = instance.REGION;
                    cache.SROK_SVID = instance.SROK_SVID;
                    cache.StationFrequencies = instance.StationFrequencies;
                    cache.UPDATE_DATE = instance.UPDATE_DATE;
                    cache.ZAV_NUM = instance.ZAV_NUM;

                    try
                    {
                        context.SubmitChanges();
                        result = true;
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
            }

            return result;
        }

        public bool UpdateStations(IEnumerable<BroadcastStations> stations)
        {
            var result = true;

            if (stations != null)
                foreach (var station in stations)
                {
                    var nextResult = UpdateStation(station);
                    if ( !nextResult)
                    {
                        result = false;
                    }
                }

            return result;
        }
    }
}