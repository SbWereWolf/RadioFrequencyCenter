using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class LinkBroadcastStationsToResdbResRepository : Repository
    {
        public LinkBroadcastStationsToResdbResRepository()
        {
            LinkBroadcastStationsToResdbRes = RepositoryData?.LinkBroadcastStationsToResdbRes;
        }

        public Table<LinkBroadcastStationsToResdbRes> LinkBroadcastStationsToResdbRes { get; }

        public bool InsertStation(LinkBroadcastStationsToResdbRes instance)
        {
            var result = false;
            var linkBroadcastStationsToResdbRes = LinkBroadcastStationsToResdbRes;
            var context = linkBroadcastStationsToResdbRes?.Context;

            if (instance != null && context != null)
            {
                linkBroadcastStationsToResdbRes.InsertOnSubmit(instance);

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

        public bool InsertStations(IEnumerable<LinkBroadcastStationsToResdbRes> stations)
        {
            var result = false;
            var linkBroadcastStationsToResdbRes = LinkBroadcastStationsToResdbRes;
            var context = linkBroadcastStationsToResdbRes?.Context;

            if (linkBroadcastStationsToResdbRes != null && context != null && stations != null)
            {
                linkBroadcastStationsToResdbRes.InsertAllOnSubmit(stations);

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