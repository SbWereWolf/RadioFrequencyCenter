using System;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class BroadcastFrequenciesRepository : Repository
    {
        public BroadcastFrequenciesRepository()
        {
            BroadcastFrequencies = RepositoryData?.BroadcastFrequencies;
        }

        public Table<BroadcastFrequencies> BroadcastFrequencies { get; }

        public bool InsertFrequency(BroadcastFrequencies instance)
        {
            var result = false;
            var frequencies = BroadcastFrequencies;
            var context = frequencies?.Context;
            if (instance != null && context != null)
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
            var frequencies = BroadcastFrequencies;
            var context = frequencies?.Context;
            if (instance != null && context != null)
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

            var frequencies = BroadcastFrequencies;
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
    }
}