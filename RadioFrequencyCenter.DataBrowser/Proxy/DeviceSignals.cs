using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class DeviceSignals
    {
        public BroadcastFrequenciesRepository Repository { get; }

        public DeviceSignals()
        {
            var broadcastFrequenciesRepository = new BroadcastFrequenciesRepository();
            Repository = broadcastFrequenciesRepository;
        }
    }
}