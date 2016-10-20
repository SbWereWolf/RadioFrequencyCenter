using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class LinkStationFrequenciesToResdbFrqRepository : Repository
    {
        public LinkStationFrequenciesToResdbFrqRepository()
        {
            LinkStationFrequenciesToResdbFrq = RepositoryData?.LinkStationFrequenciesToResdbFrq;
        }

        public Table<LinkStationFrequenciesToResdbFrq> LinkStationFrequenciesToResdbFrq { get; }

        public bool InsertStation(LinkStationFrequenciesToResdbFrq instance)
        {
            var result = false;
            var linkStationFrequenciesToResdbFrq = LinkStationFrequenciesToResdbFrq;
            var context = linkStationFrequenciesToResdbFrq?.Context;

            if (instance != null && context != null)
            {
                linkStationFrequenciesToResdbFrq.InsertOnSubmit(instance);

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

        public bool InsertFrequrencies(IEnumerable<LinkStationFrequenciesToResdbFrq> stations)
        {
            var result = false;
            var linkStationFrequenciesToResdbFrq = LinkStationFrequenciesToResdbFrq;
            var context = linkStationFrequenciesToResdbFrq?.Context;

            if (linkStationFrequenciesToResdbFrq != null && context != null && stations != null)
            {
                linkStationFrequenciesToResdbFrq.InsertAllOnSubmit(stations);

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