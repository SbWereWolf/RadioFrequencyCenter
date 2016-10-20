using System.Data.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class StationFrequenciesRepository : Repository
    {
        public StationFrequenciesRepository()
        {
            StationFrequencies = RepositoryData?.StationFrequencies;
        }

        public Table<StationFrequencies> StationFrequencies { get; }

        //public bool InsertFrequency(StationFrequencies instance)
        //{
        //    var result = false;
        //    var frequencies = StationFrequencies;
        //    var context = frequencies?.Context;
        //    if (instance != null && context != null)
        //    {
        //        frequencies.InsertOnSubmit(instance);
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

        //public bool UpdateFrequency(StationFrequencies instance)
        //{
        //    var result = false;
        //    var frequencies = StationFrequencies;
        //    var context = frequencies?.Context;
        //    if (instance != null && context != null)
        //    {
        //        var cache = frequencies.FirstOrDefault(p => p.ID_F == instance.ID_F);
        //        if (cache != null)
        //        {
        //            cache.RN = instance.RN;
        //            cache.TN = instance.TN;

        //            try
        //            {
        //                context.SubmitChanges();
        //                result = true;
        //            }
        //            catch (Exception)
        //            {
        //                //throw;
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public bool DeleteAllFrequency()
        //{
        //    var result = false;

        //    var frequencies = StationFrequencies;
        //    var context = frequencies?.Context;
        //    if (context != null)
        //    {
        //        frequencies.DeleteAllOnSubmit(frequencies);
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
    }
}