using System;
using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class BroadcastStations
    {
        public BroadcastStations()
        {
            var broadcastStationsRepository = new DataAccessLayer.BroadcastStationsRepository();
            Repository = broadcastStationsRepository;
        }

        public BroadcastStationsRepository Repository { get; }

        public DataAccessLayer.BroadcastStations[] GetData()
        {
            DataAccessLayer.BroadcastStations[] data = null;
            if (Repository?.RepositoryData?.BroadcastStations != null)
            {
                data = Repository.RepositoryData.BroadcastStations.ToArray();
                
            }
            return data;
        }

        public bool DeleteAll()
        {
            var result = false;
            var broadcastStations  = Repository?.RepositoryData?.BroadcastStations;
            var context = broadcastStations?.Context;
            if ( context != null )
            {
                broadcastStations.DeleteAllOnSubmit(broadcastStations.ToArray());

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