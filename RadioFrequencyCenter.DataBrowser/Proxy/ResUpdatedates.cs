﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using RadioFrequencyCenter.DataBrowser.DataAccessLayer;
using RadioFrequencyCenter.DataBrowser.Models;

namespace RadioFrequencyCenter.DataBrowser.Proxy
{
    public class ResUpdatedates
    {
        public ResUpdatedatesRepository Repository { get; }

        public ResUpdatedates()
        {
            var resUpdatedatesRepository = new ResUpdatedatesRepository();
            Repository = resUpdatedatesRepository;
        }

        public List<RadioDevice> GetForUpdate(List<RadioDevice> radioDevicesList)
        {
            List<RadioDevice> radioDevices = null;

            var resUpdatedates = Repository?.ResUpdatedates;
            if (radioDevicesList != null && resUpdatedates != null)
            {
                radioDevices =
                    radioDevicesList?.Where(
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

            return radioDevices;
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