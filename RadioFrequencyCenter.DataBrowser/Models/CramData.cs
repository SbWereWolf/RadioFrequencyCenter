﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Ajax.Utilities;
using RadioFrequencyCenter.DataBrowser.MeasurementsApiService;
using RadioFrequencyCenter.DataBrowser.Proxy;
using RadioFrequencyCenter.DataSource;

namespace RadioFrequencyCenter.DataBrowser.Models
{
    public class CramData
    {
        public CramData()
        {
            SelectionCriteria = new SelectionCriteria();
        }

        public SelectionCriteria SelectionCriteria { get; set;  }

        private bool GetIt()
        {
            var result = false;
            var apiClient = new MeasurementsApiClient();
            var rawRecords = apiClient.GetRadioStationsAndSignals(SelectionCriteria);

            var radioDevices = new List<RadioDevice>();
            if (rawRecords != null)
            {
                foreach (var rawRecord in rawRecords)
                {
                    if (rawRecord != null)
                    {
                        var instance = new RadioDevice
                        {
                            DateSvid = rawRecord.CertificateIssueDate,
                            DelDate = rawRecord.DelDate,
                            Ids = $" Bsid : {rawRecord.Bsid} ; Ci : {rawRecord.Ci} ; Lac : {rawRecord.Lac} ; ",
                            Lat = rawRecord.LocationLattitude,
                            Long = rawRecord.LocationLongitude,
                            Mac = rawRecord.Mac,
                            NumSvid = rawRecord.CertificateNumber,
                            Region= rawRecord.SpRegionGai,
                            SrokSvid= rawRecord.CertificateValidDate,
                            UpdateDate= rawRecord.UpdateDate,
                            ZavNum= rawRecord.FactoryNumber?.ToStringInvariant(),
                            RadioSignals= new RadioDevice.DeviceSignal[] {},
                            Guid = rawRecord.Guid
                        };

                        var signalsFrequencies = rawRecord.SignalsFrequencies;
                        if (signalsFrequencies?.Length > 0)
                        {
                            var frequencies = new List<RadioDevice.DeviceSignal>();

                            foreach (var frequrency in signalsFrequencies)
                            {
                                if (frequrency != null)
                                {
                                    var signalFrequency = new RadioDevice.DeviceSignal
                                    {
                                        Rn= frequrency.Rn,
                                        Tn= frequrency.Tn,
                                        Guid = frequrency.Guid
                                    };
                                    frequencies.Add(signalFrequency);
                                }
                            }
                            if (frequencies.Count > 0)
                            {
                                var frequenciesArray = frequencies.ToArray();
                                instance.RadioSignals = frequenciesArray;
                            }
                        }

                        radioDevices.Add(instance);
                    }
                }
            }

            var guidProxy = new ResUpdatedates();

            var radioDevicesForUpdate = guidProxy.GetForUpdate(radioDevices);
            List<RadioDevice> radioDevicesForInsert = guidProxy.GetForInsert(radioDevices);


            var devicesProxy = new RadioDevices();
            if (devicesProxy.Repository != null
                && radioDevicesForInsert!= null)
            {
                
                var isSuccess = devicesProxy.InsertStations(radioDevicesForInsert);
                if (isSuccess)
                {
                    result = true;
                }
            }
            if (radioDevicesForUpdate != null)
            {
                if (radioDevicesForUpdate.Any())
                {
                    var isSuccess = devicesProxy.UpdateStations(radioDevicesForUpdate);
                    result = isSuccess && result;
                }
            }

            return result;
        }

        public bool NewerOnly()
        {
            var proxy = new RadioDevices();
            var dateupdate = proxy.GetLastUpdateDate();
            
            var stationCriteria = SelectionCriteria?.Station;
            if (stationCriteria != null && dateupdate.HasValue)
            {
                stationCriteria.UpdateDate = dateupdate.Value.LocalDateTime;
            }
            var result = GetIt();
            return result;
        }
        public bool WholeData()
        {
            var result = GetIt();
            return result;
        }

        public bool DevicesByParameters(NameValueCollection requestForm)
        {
            var result = false;
            if (requestForm != null)
            {
                var certificatenumber = requestForm["SelectionCriteria.Station.CertificateNumber"];
                var factorynumber = requestForm["SelectionCriteria.Station.FactoryNumber"];
                
                var selectionCriteria = SelectionCriteria?.Station;
                if (selectionCriteria != null)
                {
                    selectionCriteria.CertificateNumber = certificatenumber;
                    selectionCriteria.FactoryNumber = null;
                    int factoryNumberInt;
                    var isConverted = int.TryParse(factorynumber, out factoryNumberInt);
                    if (isConverted)
                    {
                        selectionCriteria.FactoryNumber = factoryNumberInt;
                    }
                }

                result = GetIt();
            }
            return result;
        }
    }
}
