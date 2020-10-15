using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using YPIConnect.Persistence;

namespace YPIConnect
{
    public class DistributionLog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_TestOrderReportDistributionId;
        private string m_ReportNo;
        private string m_PhysicianName;
        private string m_PhysicianId;
        private string m_ClientName;
        private string m_ClientId;
        private string m_PLastName;
        private string m_PFirstName;
        private DateTime m_PBirthdate;
        private string m_DistributionType;
        private bool m_Distributed;
        private Nullable<DateTime> m_TimeOfLastDistribution;

        public DistributionLog()
        {

        }

        [ListProperty("TestOrderReportDistributionId")]
        public string TestOrderReportDistributionId
        {
            get { return this.m_TestOrderReportDistributionId; }
            set
            {
                if (this.m_TestOrderReportDistributionId != value)
                {
                    this.m_TestOrderReportDistributionId = value;
                    this.NotifyPropertyChanged("TestOrderReportDistributionId");
                }
            }
        }

        [ListProperty("ReportNo")]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [ListProperty("PhysicianName")]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (this.m_PhysicianName != value)
                {
                    this.m_PhysicianName = value;
                    this.NotifyPropertyChanged("PhysicianName");
                }
            }
        }

        [ListProperty("PhysicianId")]
        public string PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;
                    this.NotifyPropertyChanged("PhysicianId");
                }
            }
        }

        [ListProperty("ClientName")]
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

        [ListProperty("ClientId")]
        public string ClientId
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

        [ListProperty("PLastName")]
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

        [ListProperty("PFirstName")]
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

        [ListProperty("PBirthdate")]
        public DateTime PBirthdate
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

        [ListProperty("DistributionType")]
        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set
            {
                if (this.m_DistributionType != value)
                {
                    this.m_DistributionType = value;
                    this.NotifyPropertyChanged("DistributionType");
                }
            }
        }

        [ListProperty("Distributed")]
        public bool Distributed
        {
            get { return this.m_Distributed; }
            set
            {
                if (this.m_Distributed != value)
                {
                    this.m_Distributed = value;
                    this.NotifyPropertyChanged("Distributed");
                }
            }
        }

        [ListProperty("TimeOfLastDistribution")]
        public Nullable<DateTime> TimeOfLastDistribution
        {
            get { return this.m_TimeOfLastDistribution; }
            set
            {
                if (this.m_TimeOfLastDistribution != value)
                {
                    this.m_TimeOfLastDistribution = value;
                    this.NotifyPropertyChanged("TimeOfLastDistribution");
                }
            }
        }

        public string GetXPSFilePath()
        {
            OrderIdParser orderIdParser = new OrderIdParser(this.m_ReportNo);
            return CaseDocumentPath.GetCaseFileNameXPS(orderIdParser);            
        }

        public void FromSql(JObject jObject)
        {
            Type type = typeof(DistributionLog);
            JsonObjectWriter.WriteListObject(type, jObject, this);
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
