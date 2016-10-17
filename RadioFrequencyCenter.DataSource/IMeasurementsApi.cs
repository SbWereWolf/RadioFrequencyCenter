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
        RadioDevice[] GetRadioDevicesData(SelectionCriteria selectionCriteria);
        
    }
    
    [DataContract]
    public class SelectionCriteria
    {
        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTill { get; set; } 
    }

    [DataContract]
    public class RadioDevice
    {
        [DataMember]
        public Guid Guid { get; set; }

        [DataMember]
        public int FactoryNumber { get; set; }

        [DataMember]
        public string CertificateNumber { get; set; }

        [DataMember]
        public DateTime? CertificateIssueDate { get; set; }

        [DataMember]
        public DateTime? CertificateValidDate { get; set; }

        [DataMember]
        public double SpRegionGai { get; set; }

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
        public int IsDeleted { get; set; }

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
        public Guid Res { get; set; }

        [DataMember]
        public double Tn { get; set; }

        [DataMember]
        public double Rn { get; set; }
    }
}
