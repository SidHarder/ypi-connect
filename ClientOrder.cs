using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using YPIConnect.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    [PersistentClass("tblClientOrder")]
    public class ClientOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ClientOrderDetailCollection m_ClientOrderDetailCollection;

        private RowOperationTypeEnum m_RowOperationType;
        private string m_ClientOrderId;
        private bool m_Received;
        private bool m_Submitted;
        private Nullable<DateTime> m_OrderDate;
        private Nullable<DateTime> m_OrderTime;
        private string m_OrderedBy;
        private string m_PFirstName;
        private string m_PLastName;
        private string m_PMiddleInitial;
        private Nullable<DateTime> m_PBirthdate;
        private string m_PSex;
        private string m_PSSN;
        private string m_PAddress1;
        private string m_PAddress2;
        private string m_PCity;
        private string m_PState;
        private string m_PZipCode;
        private string m_SvhMedicalRecord;
        private string m_SvhAccountNo;
        private int m_ClientId;
        private string m_ClientName;
        private Nullable<int> m_ClientLocationId;
        private string m_ProviderId;
        private string m_ProviderName;
        private string m_ClinicalHistory;
        private string m_ReportCopyTo;
        private string m_OrderType;
        private Nullable<int> m_PanelSetId;
        private bool m_Accessioned;
        private bool m_Validated;
        private Nullable<DateTime> m_CollectionDate;
        private string m_ExternalOrderId;
        private string m_SecondaryExternalOrderId;
        private string m_IncomingHL7;
        private string m_MasterAccessionNo;
        private string m_OrderedByFirstName;
        private string m_OrderedByLastName;
        private string m_OrderedById;
        private string m_ProviderFirstName;
        private string m_ProviderLastName;
        private bool m_Hold;
        private string m_HoldMessage;
        private string m_HoldBy;
        private bool m_Acknowledged;
        private int m_AcknowledgedById;
        private Nullable<DateTime> m_AcknowledgedDate;
        private string m_SystemInitiatingOrder;
        private string m_LocationCode;
        private string m_SpecialInstructions;
        private string m_UniversalServiceId;
        private string m_OrderStatus;
        private string m_PatientType;
        private string m_ElectronicOrderType;
        private string m_PlaceOfService;
        
        public ClientOrder()
        {
            //this.m_ClientOrderDetailCollection = new ClientOrderDetailCollection();
        }

        //public ClientOrderDetailCollection ClientOrderDetailCollection
        //{
        //    get { return this.m_ClientOrderDetailCollection; }
        //    set { this.m_ClientOrderDetailCollection = value; }
        //}

        public RowOperationTypeEnum RowOperationType
        {
            get { return this.m_RowOperationType; }
            set { this.m_RowOperationType = value; }
        }

        [PersistentPrimaryKeyProperty()]
        [PersistentProperty("ClientOrderId")]
        public string ClientOrderId
        {
            get { return this.m_ClientOrderId; }
            set
            {
                if (this.m_ClientOrderId != value)
                {
                    this.m_ClientOrderId = value;
                    this.NotifyPropertyChanged("ClientOrderId");
                }
            }
        }
        
        [PersistentProperty("Received")]
        public bool Received
        {
            get { return this.m_Received; }
            set
            {
                if (this.m_Received != value)
                {
                    this.m_Received = value;
                    this.NotifyPropertyChanged("Received");
                }
            }
        }
        
        [PersistentProperty("Submitted")]
        public bool Submitted
        {
            get { return this.m_Submitted; }
            set
            {
                if (this.m_Submitted != value)
                {
                    this.m_Submitted = value;
                    this.NotifyPropertyChanged("Submitted");
                }
            }
        }       
        
        [PersistentProperty("OrderDate")]
        public Nullable<DateTime> OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (this.m_OrderDate != value)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
                }
            }
        }

        
        
        [PersistentProperty("OrderTime")]
        public Nullable<DateTime> OrderTime
        {
            get { return this.m_OrderTime; }
            set
            {
                if (this.m_OrderTime != value)
                {
                    this.m_OrderTime = value;
                    this.NotifyPropertyChanged("OrderTime");
                }
            }
        }

        
        
        [PersistentProperty("OrderedBy")]
        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
            set
            {
                if (this.m_OrderedBy != value)
                {
                    this.m_OrderedBy = value;
                    this.NotifyPropertyChanged("OrderedBy");
                }
            }
        }

        
        
        [PersistentProperty("PFirstName")]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }        
        
        [PersistentProperty("PLastName")]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        
        
        [PersistentProperty("PMiddleInitial")]
        public string PMiddleInitial
        {
            get { return this.m_PMiddleInitial; }
            set
            {
                if (this.m_PMiddleInitial != value)
                {
                    this.m_PMiddleInitial = value;
                    this.NotifyPropertyChanged("PMiddleInitial");
                }
            }
        }

        
        
        [PersistentProperty("PBirthdate")]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set
            {
                if (this.m_PBirthdate != value)
                {
                    this.m_PBirthdate = value;
                    this.NotifyPropertyChanged("PBirthdate");
                }
            }
        }        
        
        [PersistentProperty("PSex")]
        public string PSex
        {
            get { return this.m_PSex; }
            set
            {
                if (this.m_PSex != value)
                {
                    this.m_PSex = value;
                    this.NotifyPropertyChanged("PSex");
                }
            }
        }

        
        
        [PersistentProperty("PSSN")]
        public string PSSN
        {
            get { return this.m_PSSN; }
            set
            {
                if (this.m_PSSN != value)
                {
                    this.m_PSSN = value;
                    this.NotifyPropertyChanged("PSSN");
                }
            }
        }

        
        
        [PersistentProperty("SvhMedicalRecord")]
        public string SvhMedicalRecord
        {
            get { return this.m_SvhMedicalRecord; }
            set
            {
                if (this.m_SvhMedicalRecord != value)
                {
                    this.m_SvhMedicalRecord = value;
                    this.NotifyPropertyChanged("SvhMedicalRecord");
                }
            }
        }        
        
        [PersistentProperty("SvhAccountNo")]
        public string SvhAccountNo
        {
            get { return this.m_SvhAccountNo; }
            set
            {
                if (this.m_SvhAccountNo != value)
                {
                    this.m_SvhAccountNo = value;
                    this.NotifyPropertyChanged("SvhAccountNo");
                }
            }
        }        
        
        [PersistentProperty("ClientId")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }        
        
        [PersistentProperty("ClientName")]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }        
        
        [PersistentProperty("ClientLocationId")]
        public Nullable<int> ClientLocationId
        {
            get { return this.m_ClientLocationId; }
            set
            {
                if (this.m_ClientLocationId != value)
                {
                    this.m_ClientLocationId = value;
                    this.NotifyPropertyChanged("ClientLocationId");
                }
            }
        }        
        
        [PersistentProperty("ProviderId")]
        public string ProviderId
        {
            get { return this.m_ProviderId; }
            set
            {
                if (this.m_ProviderId != value)
                {
                    this.m_ProviderId = value;
                    this.NotifyPropertyChanged("ProviderId");
                }
            }
        }        
        
        [PersistentProperty("ProviderName")]
        public string ProviderName
        {
            get { return this.m_ProviderName; }
            set
            {
                if (this.m_ProviderName != value)
                {
                    this.m_ProviderName = value;
                    this.NotifyPropertyChanged("ProviderName");
                }
            }
        }        
        
        [PersistentProperty("ClinicalHistory")]
        public string ClinicalHistory
        {
            get { return this.m_ClinicalHistory; }
            set
            {
                if (this.m_ClinicalHistory != value)
                {
                    this.m_ClinicalHistory = value;
                    this.NotifyPropertyChanged("ClinicalHistory");
                }
            }
        }        
        
        [PersistentProperty("ReportCopyTo")]
        public string ReportCopyTo
        {
            get { return this.m_ReportCopyTo; }
            set
            {
                if (this.m_ReportCopyTo != value)
                {
                    this.m_ReportCopyTo = value;
                    this.NotifyPropertyChanged("ReportCopyTo");
                }
            }
        }        
        
        [PersistentProperty("OrderType")]
        public string OrderType
        {
            get { return this.m_OrderType; }
            set
            {
                if (this.m_OrderType != value)
                {
                    this.m_OrderType = value;
                    this.NotifyPropertyChanged("OrderType");
                }
            }
        }        
        
        [PersistentProperty("PanelSetId")]
        public Nullable<int> PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    this.NotifyPropertyChanged("PanelSetId");
                }
            }
        }        
        
        [PersistentProperty("Accessioned")]
        public bool Accessioned
        {
            get { return this.m_Accessioned; }
            set
            {
                if (this.m_Accessioned != value)
                {
                    this.m_Accessioned = value;
                    this.NotifyPropertyChanged("Accessioned");
                }
            }
        }        
        
        [PersistentProperty("Validated")]
        public bool Validated
        {
            get { return this.m_Validated; }
            set
            {
                if (this.m_Validated != value)
                {
                    this.m_Validated = value;
                    this.NotifyPropertyChanged("Validated");
                }
            }
        }        
        
        [PersistentProperty("CollectionDate")]
        public Nullable<DateTime> CollectionDate
        {
            get { return this.m_CollectionDate; }
            set
            {
                if (this.m_CollectionDate != value)
                {
                    this.m_CollectionDate = value;
                    this.NotifyPropertyChanged("CollectionDate");
                }
            }
        }        
        
        [PersistentProperty("ExternalOrderId")]
        public string ExternalOrderId
        {
            get { return this.m_ExternalOrderId; }
            set
            {
                if (this.m_ExternalOrderId != value)
                {
                    this.m_ExternalOrderId = value;
                    this.NotifyPropertyChanged("ExternalOrderId");
                }
            }
        }        
        
        [PersistentProperty("SecondaryExternalOrderId")]
        public string SecondaryExternalOrderId
        {
            get { return this.m_SecondaryExternalOrderId; }
            set
            {
                if (this.m_SecondaryExternalOrderId != value)
                {
                    this.m_SecondaryExternalOrderId = value;
                    this.NotifyPropertyChanged("SecondaryExternalOrderId");
                }
            }
        }                       
        
        [PersistentProperty("MasterAccessionNo")]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }
               
        [PersistentProperty("OrderedByFirstName")]
        public string OrderedByFirstName
        {
            get { return this.m_OrderedByFirstName; }
            set
            {
                if (this.m_OrderedByFirstName != value)
                {
                    this.m_OrderedByFirstName = value;
                    this.NotifyPropertyChanged("OrderedByFirstName");
                }
            }
        }        
        
        [PersistentProperty("OrderedByLastName")]
        public string OrderedByLastName
        {
            get { return this.m_OrderedByLastName; }
            set
            {
                if (this.m_OrderedByLastName != value)
                {
                    this.m_OrderedByLastName = value;
                    this.NotifyPropertyChanged("OrderedByLastName");
                }
            }
        }        
        
        [PersistentProperty("OrderById")]
        public string OrderedById
        {
            get { return this.m_OrderedById; }
            set
            {
                if (this.m_OrderedById != value)
                {
                    this.m_OrderedById = value;
                    this.NotifyPropertyChanged("OrderedById");
                }
            }
        }        
        
        [PersistentProperty("ProviderFirstName")]
        public string ProviderFirstName
        {
            get { return this.m_ProviderFirstName; }
            set
            {
                if (this.m_ProviderFirstName != value)
                {
                    this.m_ProviderFirstName = value;
                    this.NotifyPropertyChanged("ProviderFirstName");
                }
            }
        }        
        
        [PersistentProperty("ProviderLastName")]
        public string ProviderLastName
        {
            get { return this.m_ProviderLastName; }
            set
            {
                if (this.m_ProviderLastName != value)
                {
                    this.m_ProviderLastName = value;
                    this.NotifyPropertyChanged("ProviderLastName");
                }
            }
        }        
        
        [PersistentProperty("Hold")]
        public bool Hold
        {
            get { return this.m_Hold; }
            set
            {
                if (this.m_Hold != value)
                {
                    this.m_Hold = value;
                    this.NotifyPropertyChanged("Hold");
                }
            }
        }        
        
        [PersistentProperty("HoldMessage")]
        public string HoldMessage
        {
            get { return this.m_HoldMessage; }
            set
            {
                if (this.m_HoldMessage != value)
                {
                    this.m_HoldMessage = value;
                    this.NotifyPropertyChanged("HoldMessage");
                }
            }
        }
               
        [PersistentProperty("HoldBy")]
        public string HoldBy
        {
            get { return this.m_HoldBy; }
            set
            {
                if (this.m_HoldBy != value)
                {
                    this.m_HoldBy = value;
                    this.NotifyPropertyChanged("HoldBy");
                }
            }
        }        
        
        [PersistentProperty("Acknowledged")]
        public bool Acknowledged
        {
            get { return this.m_Acknowledged; }
            set
            {
                if (this.m_Acknowledged != value)
                {
                    this.m_Acknowledged = value;
                    this.NotifyPropertyChanged("Acknowledged");
                }
            }
        }        
        
        [PersistentProperty("AcknowledgedById")]
        public int AcknowledgedById
        {
            get { return this.m_AcknowledgedById; }
            set
            {
                if (this.m_AcknowledgedById != value)
                {
                    this.m_AcknowledgedById = value;
                    this.NotifyPropertyChanged("AcknowledgedById");
                }
            }
        }        
        
        [PersistentProperty("AcknowledgedDate")]
        public Nullable<DateTime> AcknowledgedDate
        {
            get { return this.m_AcknowledgedDate; }
            set
            {
                if (this.m_AcknowledgedDate != value)
                {
                    this.m_AcknowledgedDate = value;
                    this.NotifyPropertyChanged("AcknowledgedDate");
                }
            }
        }

        
        
        [PersistentProperty("SystemInitiatingOrder")]
        public string SystemInitiatingOrder
        {
            get { return this.m_SystemInitiatingOrder; }
            set
            {
                if (this.m_SystemInitiatingOrder != value)
                {
                    this.m_SystemInitiatingOrder = value;
                    this.NotifyPropertyChanged("SystemInitiatingOrder");
                }
            }
        }
        
        [PersistentProperty("LocationCode")]
        public string LocationCode
        {
            get { return this.m_LocationCode; }
            set
            {
                if (this.m_LocationCode != value)
                {
                    this.m_LocationCode = value;
                    this.NotifyPropertyChanged("LocationCode");
                }
            }
        }

        [PersistentProperty("SpecialInstructions")]
        public string SpecialInstructions
        {
            get { return this.m_SpecialInstructions; }
            set
            {
                if (this.m_SpecialInstructions != value)
                {
                    this.m_SpecialInstructions = value;
                    this.NotifyPropertyChanged("SpecialInstructions");
                }
            }
        }

        [PersistentProperty("PAddress1")]
        public string PAddress1
        {
            get { return this.m_PAddress1; }
            set
            {
                if (this.m_PAddress1 != value)
                {
                    this.m_PAddress1 = value;
                    this.NotifyPropertyChanged("PAddress1");
                }
            }
        }

        [PersistentProperty("PAddress2")]
        public string PAddress2
        {
            get { return this.m_PAddress2; }
            set
            {
                if (this.m_PAddress2 != value)
                {
                    this.m_PAddress2 = value;
                    this.NotifyPropertyChanged("PAddress2");
                }
            }
        }
        
        [PersistentProperty("PCity")]
        public string PCity
        {
            get { return this.m_PCity; }
            set
            {
                if (this.m_PCity != value)
                {
                    this.m_PCity = value;
                    this.NotifyPropertyChanged("PCity");
                }
            }
        }

        [PersistentProperty("PState")]
        public string PState
        {
            get { return this.m_PState; }
            set
            {
                if (this.m_PState != value)
                {
                    this.m_PState = value;
                    this.NotifyPropertyChanged("PState");
                }
            }
        }

        [PersistentProperty("PZipCode")]
        public string PZipCode
        {
            get { return this.m_PZipCode; }
            set
            {
                if (this.m_PZipCode != value)
                {
                    this.m_PZipCode = value;
                    this.NotifyPropertyChanged("PZipCode");
                }
            }
        }
        
        [PersistentProperty("UniversalServiceId")]
        public string UniversalServiceId
        {
            get { return this.m_UniversalServiceId; }
            set
            {
                if (this.m_UniversalServiceId != value)
                {
                    this.m_UniversalServiceId = value;
                    this.NotifyPropertyChanged("UniversalServiceId");
                }
            }
        }
        
        [PersistentProperty("OrderStatus")]
        public string OrderStatus
        {
            get { return this.m_OrderStatus; }
            set
            {
                if (this.m_OrderStatus != value)
                {
                    this.m_OrderStatus = value;
                    this.NotifyPropertyChanged("OrderStatus");
                }
            }
        }

        [PersistentProperty("PatientType")]
        public string PatientType
        {
            get { return this.m_PatientType; }
            set
            {
                if (this.m_PatientType != value)
                {
                    this.m_PatientType = value;
                    this.NotifyPropertyChanged("PatientType");
                }
            }
        }
        
        [PersistentProperty("ElectronicOrderType")]
        public string ElectronicOrderType
        {
            get { return this.m_ElectronicOrderType; }
            set
            {
                if (this.m_ElectronicOrderType != value)
                {
                    this.m_ElectronicOrderType = value;
                    this.NotifyPropertyChanged("ElectronicOrderType");
                }
            }
        }
        
        [PersistentProperty("PlaceOfService")]
        public string PlaceOfService
        {
            get { return this.m_PlaceOfService; }
            set
            {
                if (this.m_PlaceOfService != value)
                {
                    this.m_PlaceOfService = value;
                    this.NotifyPropertyChanged("PlaceOfService");
                }
            }
        }

        public ClientOrder Clone()
        {
            return (ClientOrder)this.MemberwiseClone();
        }

        public APIResult Save()
        { 
            StringBuilder commandText = new StringBuilder();
            commandText.Append(SQLHelper.GetSaveCommandText(typeof(ClientOrder), this, this.m_RowOperationType));
            JObject apiRequest = APIRequestHelper.CreateSubmitSQLCommandMessage(commandText.ToString());
            APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(apiRequest);
            this.m_RowOperationType = RowOperationTypeEnum.Update;
            return apiResult;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
