using System.Collections.Generic;
using System.Linq;

namespace RadioFrequencyCenter.DataBrowser.Models
{
    public class BroadcastFrequency
    {
        public long IdF { get; set; }
        public long Res { get; set; }
        public double? Tn { get; set; }
        public double? Rn { get; set; }

        public static List<BroadcastFrequency> GetFrequencyRecords(DataAccessLayer.BroadcastStations instance)
        {
            var frequencyRecords = new List<BroadcastFrequency>();

            var broadcastFrequencies = instance?.BroadcastFrequencies;
            if (instance != null && broadcastFrequencies!= null)
            {
                var repositoryData = broadcastFrequencies.ToArray();
                
                foreach (var repositoryRecord in repositoryData)
                {
                    if (repositoryRecord != null)
                    {
                        var record = new BroadcastFrequency
                        {
                            IdF = repositoryRecord.ID_F,
                            Res= repositoryRecord.RES,
                            Rn= repositoryRecord.RN,
                            Tn= repositoryRecord.TN
                        };
                        frequencyRecords.Add(record);
                    }
                }
            }
            return frequencyRecords;
        }
    }
}