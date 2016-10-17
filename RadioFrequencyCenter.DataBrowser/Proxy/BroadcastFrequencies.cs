using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class BroadcastFrequencies
    {
        public BroadcastFrequencies()
        {
            var broadcastFrequenciesRepository = new BroadcastFrequenciesRepository();
            Repository = broadcastFrequenciesRepository;
        }

        public BroadcastFrequenciesRepository Repository { get; }

        public DataAccessLayer.BroadcastFrequencies[] GetData()
        {
            DataAccessLayer.BroadcastFrequencies[] data = null;
            if (Repository?.BroadcastFrequencies != null)
            {
                data = Repository.BroadcastFrequencies.ToArray();
            }
            return data;
        }
    }
}