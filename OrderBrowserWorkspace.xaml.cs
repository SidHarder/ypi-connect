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
    public partial class OrderBrowserWorkspace : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TabControl m_MainWindowTabControl;        

        private Uri m_CurrentPackageUri;

        public OrderBrowserWorkspace(TabControl mainWindowTabControl)
        {
            this.m_MainWindowTabControl = mainWindowTabControl;            
            InitializeComponent();

            this.DataContext = this;
        }
                
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        

        private void ToolBarButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            ClientOrderEditDialog clientOrderEditDialog = new ClientOrderEditDialog();
            clientOrderEditDialog.ShowDialog();
        }

        private void ToolBarButtonEdit_Click(object sender, RoutedEventArgs e)
        {

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

        private void ButtonPatientNameSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRequisitionIdSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxPatientNameSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void TextBoxRequisitionIdSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ListViewOrderLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewAccessionLogColumnHeader_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
