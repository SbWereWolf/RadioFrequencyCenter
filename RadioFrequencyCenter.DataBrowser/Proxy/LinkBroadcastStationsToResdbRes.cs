using RadioFrequencyCenter.DataBrowser.DataAccessLayer;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class LinkBroadcastStationsToResdbRes
    {
        public LinkBroadcastStationsToResdbResRepository Repository { get; }

        public LinkBroadcastStationsToResdbRes()
        {
            var linkBroadcastStationsToResdbResRepository = new LinkBroadcastStationsToResdbResRepository();
            Repository = linkBroadcastStationsToResdbResRepository;
        }
    }
}