using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Net;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    public partial class ReportBrowserWorkspace : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TabControl m_MainWindowTabControl;

        private string m_PatientNameSearch;
        private string m_CategorySearchType;
        private Nullable<DateTime> m_DateOfBirthSearch;

        private DistributionLogCollection m_DistributionLog;

        private Uri m_CurrentPackageUri;

        public ReportBrowserWorkspace(TabControl mainWindowTabControl)
        {
            this.m_MainWindowTabControl = mainWindowTabControl;
            this.m_CategorySearchType = "Recent Cases";

            InitializeComponent();

            this.DataContext = this;
        }

        public DistributionLogCollection DistributionLog
        {
            get { return this.m_DistributionLog; }
        }

        public string PatientNameSearch
        {
            get { return this.m_PatientNameSearch; }
            set
            {
                if (this.m_PatientNameSearch != value)
                {
                    this.m_PatientNameSearch = value;
                    this.NotifyPropertyChanged("PatientNameSearch");
                }
            }
        }

        public Nullable<DateTime> DateOfBirthSearch
        {
            get { return this.m_DateOfBirthSearch; }
            set
            {
                if (this.m_DateOfBirthSearch != value)
                {
                    this.m_DateOfBirthSearch = value;
                    this.NotifyPropertyChanged("DateOfBirthSearch");
                }
            }
        }

        public string CategorySearchType
        {
            get { return this.m_CategorySearchType; }
            set
            {
                if (this.m_CategorySearchType != value)
                {
                    this.m_CategorySearchType = value;
                    this.NotifyPropertyChanged("CategorySearchType");
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCategorySearchType_Click(object sender, RoutedEventArgs e)
        {
            DoCategoryTypeSearch();
        }

        private void DoCategoryTypeSearch()
        {            
            if (this.m_CategorySearchType == "Not Acknowledged")
            {
                MethodResult methodResult = DistributionLogCollection.GetByNotAcknowledged();
                if (methodResult.Success == true)
                {
                    this.m_DistributionLog = (DistributionLogCollection)methodResult.Result;
                    this.NotifyPropertyChanged(string.Empty);
                }
                else
                {
                    methodResult.ShowMethodResult();
                }
            }                        
            else if(this.m_CategorySearchType == "Recent Cases")
            {
                MethodResult methodResult = DistributionLogCollection.GetByRecentCases();
                if (methodResult.Success == true)
                {
                    this.m_DistributionLog = (DistributionLogCollection)methodResult.Result;
                    this.NotifyPropertyChanged(string.Empty);
                }
                else
                {
                    methodResult.ShowMethodResult();
                }
            }
        }

        private void ButtonPatientNameSearch_Click(object sender, RoutedEventArgs e)
        {
            DoPatientNameSearch();
        }

        private void DoPatientNameSearch()
        {
            PatientName patientName = null;
            bool result = PatientName.TryParse(this.m_PatientNameSearch, out patientName);
            if (result)
            {
                MethodResult methodResult = DistributionLogCollection.GetByPatientName(patientName);                
                if (methodResult.Success == true)
                {
                    this.m_DistributionLog = (DistributionLogCollection)methodResult.Result;
                    this.NotifyPropertyChanged(string.Empty);
                }
                else
                {
                    methodResult.ShowMethodResult();
                }
            }
            else
            {
                MessageBox.Show("Must have at least 2 characters for a patient name search.");
            }
        }

        private void ButtonDateOfBirthSearch_Click(object sender, RoutedEventArgs e)
        {
            this.DoDateOfBirthSearch();
        }

        private void DoDateOfBirthSearch()
        {
            if(this.m_DateOfBirthSearch.HasValue == true)
            {
                MethodResult methodResult = DistributionLogCollection.GetByDateOfBirth(this.m_DateOfBirthSearch.Value);
                if (methodResult.Success == true)
                {
                    this.m_DistributionLog = (DistributionLogCollection)methodResult.Result;
                    this.NotifyPropertyChanged(string.Empty);
                }
                else
                {
                    methodResult.ShowMethodResult();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valide Date Of Birth.");
            }
            
        }

        private void TextBoxPatientName_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void TextBoxDateOfBirthSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewReportDistributionLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewReportDistributionLog.SelectedItems.Count != 0)
            {
                DistributionLog distributionLog = (DistributionLog)this.ListViewReportDistributionLog.SelectedItem;
                string fileName = distributionLog.GetXPSFilePath();

                JObject jsonRequest = APIRequestHelper.GetCaseDocumentMessage(fileName);
                APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);

                int count = apiResult.JSONResult["result"]["message"]["data"].Count();
                byte[] buffer = new byte[count];
                for(int i=0; i<count; i++)
                {                    
                    buffer[i] = (byte)Convert.ToInt32(apiResult.JSONResult["result"]["message"]["data"][i].ToString());
                }

                if(this.m_CurrentPackageUri != null)
                {
                    PackageStore.RemovePackage(this.m_CurrentPackageUri);
                }
                this.m_CurrentPackageUri = new Uri("memorystream://20-1234.S");                

                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(buffer, 0, buffer.Length);
                //responseStream.CopyTo(memoryStream);

                Package package = Package.Open(memoryStream);                
                PackageStore.AddPackage(this.m_CurrentPackageUri, package);
                XpsDocument document = new XpsDocument(package, CompressionOption.Maximum, this.m_CurrentPackageUri.ToString());
                FixedDocumentSequence fixedDocumentSequence = document.GetFixedDocumentSequence();
                this.Viewer.Document = fixedDocumentSequence as IDocumentPaginatorSource;
            }
        }        
    }
}
