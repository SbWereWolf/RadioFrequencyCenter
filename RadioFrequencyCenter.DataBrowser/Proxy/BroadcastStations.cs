using System;
using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class BroadcastStations
    {
        public BroadcastStations()
        {
            var broadcastStationsRepository = new BroadcastStationsRepository();
            Repository = broadcastStationsRepository;
        }

        public BroadcastStationsRepository Repository { get; }

        public DataAccessLayer.BroadcastStations[] GetData()
        {
            DataAccessLayer.BroadcastStations[] data = null;
            if (Repository?.BroadcastStations != null)
            {
                data = Repository.BroadcastStations.ToArray();
            }
            return data;
        }

        public bool DeleteAll()
        {
            var result = false;

            var broadcastStations  = Repository?.BroadcastStations;
            var stationsContext = broadcastStations?.Context;

            var frequencies = new BroadcastFrequencies();
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
    }
}