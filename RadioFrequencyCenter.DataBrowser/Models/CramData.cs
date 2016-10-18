using System.Collections.Generic;
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

        public bool GetIt()
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
                            RadioSignals= new RadioDevice.SignalFrequency[] {}
                        };

                        var signalsFrequencies = rawRecord.SignalsFrequencies;
                        if (signalsFrequencies?.Length > 0)
                        {
                            var frequencies = new List<RadioDevice.SignalFrequency>();

                            foreach (var frequrency in signalsFrequencies)
                            {
                                if (frequrency != null)
                                {
                                    var signalFrequency = new RadioDevice.SignalFrequency
                                    {
                                        Rn= frequrency.Rn,
                                        Tn= frequrency.Tn
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

            var proxy = new RadioDevices();
            if (proxy.Repository != null)
            {
                var isSuccess = proxy.InsertStations(radioDevices);
                if (isSuccess)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
