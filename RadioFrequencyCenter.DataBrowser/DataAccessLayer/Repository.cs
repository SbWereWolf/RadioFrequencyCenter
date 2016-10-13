using System;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class Repository : IRepository
    {
        public RadioFrequencyCenterDataContext RepositoryData { get; set; }

        #region BroadcastStation

        public IQueryable<BroadcastStations> BroadcastStations
        {
            get
            {
                Table<BroadcastStations> result = null;
                if (RepositoryData != null)
                {
                    result = RepositoryData.BroadcastStations;
                    
                }
                return result;
            }
        }

        public bool InsertStation(BroadcastStations instance)
        {
            var result = false;
            var stations = RepositoryData?.BroadcastStations;
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

        public bool UpdateStation(BroadcastStations instance)
        {
            var result = false;
            var stations = RepositoryData?.BroadcastStations ;
            var context = stations?.Context;
            if (instance != null && context != null )
            {
                var cache = stations.FirstOrDefault(p => p.ID_RES == instance.ID_RES);
                if (cache != null)
                {
                    cache.DATE_SVID= instance.DATE_SVID;
                    cache.DEL_DATE = instance.DEL_DATE;
                    cache.IDS = instance.IDS;
                    cache.LAT= instance.LAT;
                    cache.LONG= instance.LONG;
                    cache.MAC= instance.MAC;
                    cache.NUM_SVID= instance.NUM_SVID;
                    cache.REGION= instance.REGION;
                    cache.SROK_SVID= instance.SROK_SVID;

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

        public bool DeleteAllStation()
        {
            var result = false;

            var stations = RepositoryData?.BroadcastStations;
            var context = stations?.Context;
            if (context != null )
            {
                stations.DeleteAllOnSubmit(stations);
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

        #endregion

        #region BroadcastFrequency

        public IQueryable<BroadcastFrequencies> BroadcastFrequencies
        {
            get
            {
                Table<BroadcastFrequencies> result = null;
                if (RepositoryData != null)
                {
                    result = RepositoryData.BroadcastFrequencies;

                }
                return result;
            }
        }

        public bool InsertFrequency(BroadcastFrequencies instance)
        {
            var result = false;
            var frequencies = RepositoryData?.BroadcastFrequencies;
            var context = frequencies?.Context;
            if (instance != null && context != null )
            {
                frequencies.InsertOnSubmit(instance);
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

        public bool UpdateFrequency(BroadcastFrequencies instance)
        {
            var result = false;
            var frequencies = RepositoryData?.BroadcastFrequencies;
            var context = frequencies?.Context;
            if (instance != null && context != null )
            {
                var cache = frequencies.FirstOrDefault(p => p.ID_F == instance.ID_F);
                if (cache != null)
                {
                    cache.RN = instance.RN;
                    cache.TN = instance.TN;

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

        public bool DeleteAllFrequency()
        {
            var result = false;

            var frequencies = RepositoryData?.BroadcastFrequencies;
            var context = frequencies?.Context;
            if (context != null)
            {
                frequencies.DeleteAllOnSubmit(frequencies);
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

        #endregion
    }
}