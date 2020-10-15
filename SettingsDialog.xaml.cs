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
using System.Windows.Shapes;
using QRCoder;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    /// <summary>
    /// Interaction logic for QRCodeGeneratorDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LocalSettings m_LocalSettings;
        private bool m_FormPropertiesAreEnabled;

        public SettingsDialog()
        {            
            InitializeComponent();
            this.DataContext = this;

            if (LocalSettings.AreLocalSettingsConfigured() == true)
            {
                this.m_LocalSettings = LocalSettings.Instance;
                this.m_FormPropertiesAreEnabled = true;

                if (string.IsNullOrEmpty(this.m_LocalSettings.QRCodeImage) == false)
                    this.LoadQRCodeImage(this.m_LocalSettings.QRCodeImage);
            }
            else
            {
                this.m_FormPropertiesAreEnabled = false;
            }

            this.NotifyPropertyChanged(string.Empty);
        }        

        public LocalSettings LocalSettings
        {
            get { return this.m_LocalSettings; }
        }

        public bool FormPropertiesAreEnabled
        {
            get { return this.m_FormPropertiesAreEnabled; }
            set
            {
                if (this.m_FormPropertiesAreEnabled != value)
                {
                    this.m_FormPropertiesAreEnabled = value;
                    this.NotifyPropertyChanged("FormPropertiesAreEnabled");
                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_LocalSettings != null)
            {
                this.m_LocalSettings.Serialize();                
            }
            this.Close();
        }

        public static Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;
            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        private void CreateSettings_Click(object sender, RoutedEventArgs e)
        {
            LocalSettings.SetupLocalSettings();
            this.m_LocalSettings = LocalSettings.Instance;
            this.m_FormPropertiesAreEnabled = true;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        

        private void HyperLinkGetQRCode_Click(object sender, RoutedEventArgs e)
        {
            JObject apiRequest = APIRequestHelper.GetQRCodeMessage(this.m_LocalSettings.UserName, this.m_LocalSettings.Password);
            APIResult apiResponse = APIRequestHelper.SubmitAPIRequestMessage(apiRequest);

            string base64String = apiResponse.JSONResult["result"]["qrCodeImage"].ToString().Replace("data:image/png;base64,", string.Empty);
            this.m_LocalSettings.QRCodeImage = base64String;
            this.m_LocalSettings.Serialize();

            this.LoadQRCodeImage(base64String);
        }

        private void LoadQRCodeImage(string base64String)
        {
            Bitmap qrCodeImage = Base64StringToBitmap(base64String);
            MemoryStream Ms = new MemoryStream();

            qrCodeImage.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Ms.Position = 0;
            BitmapImage ObjBitmapImage = new BitmapImage();
            ObjBitmapImage.BeginInit();
            ObjBitmapImage.StreamSource = Ms;
            ObjBitmapImage.EndInit();
            this.ImageQRCode.Source = ObjBitmapImage;
        }
    }
}
