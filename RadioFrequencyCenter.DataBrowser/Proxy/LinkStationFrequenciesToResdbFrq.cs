using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class LinkStationFrequenciesToResdbFrq
    {
        public LinkStationFrequenciesToResdbFrqRepository Repository { get; }

        public LinkStationFrequenciesToResdbFrq()
        {
            var linkStationFrequenciesToResdbFrqRepository = new LinkStationFrequenciesToResdbFrqRepository();
            Repository = linkStationFrequenciesToResdbFrqRepository;
        }
    }
}