using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RadioFrequencyCenter.DataSource
{
    [ServiceContract]
    public interface IMeasurementsApi
    {
        [OperationContract]
        int IsApiOnline();

        [OperationContract]
        RadioStation[] GetRadioStationsAndSignals(SelectionCriteria selectionCriteria);
        
    }

    [DataContract]
    public class SelectionCriteria
    {
        [DataMember]
        public RadioStation Station;

        public SelectionCriteria()
        {
            Station = new RadioStation();
        }
    }

    [DataContract]
    public class RadioStation
    {
        [DataMember]
        public Guid Guid { get; set; }

        [DataMember]
        public int? FactoryNumber { get; set; }

        [DataMember]
        public string CertificateNumber { get; set; }

        [DataMember]
        public DateTime? CertificateIssueDate { get; set; }

        [DataMember]
        public DateTime? CertificateValidDate { get; set; }

        [DataMember]
        public double? SpRegionGai { get; set; }

        [DataMember]
        public string LocationLattitude { get; set; }

        [DataMember]
        public string LocationLongitude { get; set; }

        [DataMember]
        public string Lac { get; set; }

        [DataMember]
        public string Ci { get; set; }

        [DataMember]
        public string Bsid { get; set; }

        [DataMember]
        public string Mac { get; set; }

        [DataMember]
        public int? IsDeleted { get; set; }

        [DataMember]
        public DateTime? DelDate { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public SignalFrequency[] SignalsFrequencies { get; set; }
    }
    [DataContract]
    public class SignalFrequency
    {
        [DataMember]
        public Guid Guid { get; set; }

        [DataMember]
        public Guid? Res { get; set; }

        [DataMember]
        public double? Tn { get; set; }

        [DataMember]
        public double? Rn { get; set; }
    }
}
