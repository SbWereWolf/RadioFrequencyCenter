using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class DeviceSignals
    {
        public StationFrequenciesRepository Repository { get; }

        public DeviceSignals()
        {
            var stationFrequenciesRepository = new StationFrequenciesRepository();
            Repository = stationFrequenciesRepository;
        }
    }
}