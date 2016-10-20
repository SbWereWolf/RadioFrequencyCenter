using System.Collections.Generic;
using System.Linq;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class ResUpdatedates
    {
        private ResUpdatedatesRepository Repository { get; }

        public ResUpdatedates()
        {
            var resUpdatedatesRepository = new ResUpdatedatesRepository();
            Repository = resUpdatedatesRepository;
        }

        public List<RadioDevice> GetForUpdate(List<RadioDevice> radioDevicesList)
        {
            List<RadioDevice> radioDeviceList = null;

            var resUpdatedates = Repository?.ResUpdatedates;
            if (radioDevicesList != null && resUpdatedates != null)
            {
                radioDeviceList =
                    radioDevicesList.Where(
                        x =>
                            // ReSharper disable SimplifyConditionalTernaryExpression
                            x == null
                                ? false
                                : x.UpdateDate <
                                  resUpdatedates.Where(y => y.ResGUID == x.Guid.Value)
                                      .Select(z => z.UPDATE_DATE)
                                      .FirstOrDefault()
                        // ReSharper restore SimplifyConditionalTernaryExpression
                        ).ToList();

            }

            return radioDeviceList;
        }

        public List<RadioDevice> GetForInsert(List<RadioDevice> radioDevicesList)
        {
            List<RadioDevice> radioDevices = null;

            var resUpdatedates = Repository?.ResUpdatedates;
            if (radioDevicesList != null && resUpdatedates != null)
            {
                radioDevices =
                    radioDevicesList.Where(x => !resUpdatedates.Any(y => y.ResGUID == x.Guid.Value)).ToList();
            }
            return radioDevices;
        }
    }
}