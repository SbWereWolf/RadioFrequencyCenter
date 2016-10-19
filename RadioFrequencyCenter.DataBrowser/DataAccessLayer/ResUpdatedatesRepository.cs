using System;
using System.Data.Linq;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
    public class ResUpdatedatesRepository : Repository
    {
        public ResUpdatedatesRepository()
        {
            ResUpdatedates = RepositoryData?.V_ResdbResUpdatedates;
        }

        public Table<V_ResdbResUpdatedate> ResUpdatedates { get; }
    }
}