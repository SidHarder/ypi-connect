using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;

namespace YPIConnect
{
    public class LocalSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static string LocalSettingsFileName = "local_settings.json";

        private static LocalSettings m_Instance;

        private JObject m_JObject;
        
        private string m_UserName;
        private string m_Password;
        private string m_QRCodeImage;

        private LocalSettings(string json)
        {
            this.m_JObject = JObject.Parse(json);
            this.ReadPreferences();
        }

        private void ReadPreferences()
        {                        
            if (this.m_JObject["userName"] != null)
                this.m_UserName = this.m_JObject["userName"].ToString();

            if (this.m_JObject["password"] != null)
                this.m_Password = this.m_JObject["password"].ToString();

            if (this.m_JObject["qrCodeImage"] != null)
                this.m_QRCodeImage = this.m_JObject["qrCodeImage"].ToString();

            this.NotifyPropertyChanged(string.Empty);
        }

        public static LocalSettings Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    string folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string ypiConnectFolder = System.IO.Path.Combine(folder, App.YPI_Connect_Local_Data_Folder);
                    string localSettingFullPath = System.IO.Path.Combine(ypiConnectFolder, LocalSettingsFileName);
                    string jsonString = System.IO.File.ReadAllText(localSettingFullPath);
                    m_Instance = new LocalSettings(jsonString);                 
                }
                return m_Instance;
            }
        }

        public void UpdateUserNamePassword(AuthenticatedUser authenticatedUser)
        {
            this.UserName = authenticatedUser.UserName;
            this.Password = authenticatedUser.Password;
        }

        public void Serialize()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ypiConnectFolder = System.IO.Path.Combine(folder, App.YPI_Connect_Local_Data_Folder);
            string localSettingFullPath = System.IO.Path.Combine(ypiConnectFolder, LocalSettingsFileName);

            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(localSettingFullPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this.m_JObject);
            }
        }                

        public string Password
        {
            get { return this.m_Password; }
            set
            {
                if (this.m_Password != value)
                {
                    this.m_Password = value;
                    this.m_JObject["password"] = this.m_Password;
                    this.NotifyPropertyChanged("Password");
                }
            }
        }

        public string UserName
        {
            get { return this.m_UserName; }
            set
            {
                if (this.m_UserName != value)
                {
                    this.m_UserName = value;
                    this.m_JObject["userName"] = this.m_UserName;
                    this.NotifyPropertyChanged("UserName");
                }
            }
        }

        public string QRCodeImage
        {
            get { return this.m_QRCodeImage; }
            set
            {
                if (this.m_QRCodeImage != value)
                {
                    this.m_QRCodeImage = value;
                    this.m_JObject["qrCodeImage"] = this.m_QRCodeImage;
                    this.NotifyPropertyChanged("m_qrCodeImage");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        

        public static void SetupLocalSettings()
        {
            string userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ypiConnectFolder = System.IO.Path.Combine(userProfileFolder, App.YPI_Connect_Local_Data_Folder);
            string localSettingFullPath = System.IO.Path.Combine(ypiConnectFolder, LocalSettingsFileName);

            if (System.IO.Directory.Exists(ypiConnectFolder) == false)
            {
                System.IO.Directory.CreateDirectory(ypiConnectFolder);
            }

            if (System.IO.File.Exists(localSettingFullPath) == false)
            {
                JObject jObject = new JObject(
                    new JProperty("userName", null),
                    new JProperty("password", null)
                );

                using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(localSettingFullPath))
                {
                    string xx = jObject.ToString();
                    outputFile.WriteLine(jObject.ToString());
                }
            }
        }

        public static bool AreLocalSettingsConfigured()
        {
            bool result = true;
            string userProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ypiConnectFolder = System.IO.Path.Combine(userProfileFolder, App.YPI_Connect_Local_Data_Folder);
            string localSettingFullPath = System.IO.Path.Combine(ypiConnectFolder, LocalSettingsFileName);

            if (System.IO.Directory.Exists(ypiConnectFolder) == false)
            {
                result = false;
            }
            else
            {
                if (System.IO.File.Exists(localSettingFullPath) == false)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
