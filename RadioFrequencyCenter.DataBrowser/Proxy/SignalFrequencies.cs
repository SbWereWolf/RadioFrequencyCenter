using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class SignalFrequencies
    {
        public BroadcastFrequenciesRepository Repository { get; }

        public SignalFrequencies()
        {
            var broadcastFrequenciesRepository = new BroadcastFrequenciesRepository();
            Repository = broadcastFrequenciesRepository;
        }
    }
}