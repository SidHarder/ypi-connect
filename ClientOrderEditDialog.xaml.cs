using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    public partial class ClientOrderEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        public ClientOrderEditDialog()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Loaded += AccessionOrderEditDialog_Loaded;            
        }

        private void AccessionOrderEditDialog_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ButtonProviderSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxClientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBoxCopathClientId_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void TextBoxCollectionDate_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void MenuItemDistributeSelectedItems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeleteDistribution_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxDateOfService_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ComboboxTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBoxScanSimulation_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperLinkUnlockCase_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
