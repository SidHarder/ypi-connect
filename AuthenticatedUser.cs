using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    public class AuthenticatedUser
    {
        private static AuthenticatedUser m_Instance;

        private JwtSecurityToken m_Token;

        private string m_UserName;
        private string m_Password;        
        private string m_AuthenticatorToken;
        private bool m_IsAuthenticated;

        private JObject m_WebServiceAccount;
        private JArray m_WebServiceAccountClient;

        private AuthenticatedUser()
        {

        }

        public static AuthenticatedUser Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new AuthenticatedUser();
                    m_Instance.m_UserName = LocalSettings.Instance.UserName;
                    m_Instance.m_Password = LocalSettings.Instance.Password;
                }
                return m_Instance;
            }
        }

        public JwtSecurityToken Token
        {
            get { return this.m_Token; }
            set { this.m_Token = value; }
        }

        public string UserName
        {
            get { return this.m_UserName; }
            set { this.m_UserName = value; }
        }

        public string Password
        {
            get { return this.m_Password; }
            set { this.m_Password = value; }
        }
        
        public JObject WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
            set { this.m_WebServiceAccount = value; }
        }

        public JArray WebServiceAccountClient
        {
            get { return this.m_WebServiceAccountClient; }
            set { this.m_WebServiceAccountClient = value; }
        }

        public string DisplayName
        {
            get { return this.GetPayloadField("displayName");  }            
        }

        public string GetSQLClientIdInList()
        {
            StringBuilder result = new StringBuilder();
            foreach(JObject item in this.m_WebServiceAccountClient)
            {
                string clientId = item["ClientId"].ToString();
                result.Append("'" + clientId + "', ");
            }

            return result.Remove(result.Length - 2, 2).ToString();
        }

        public bool EnableReportBrowser
        {
            get
            {
                int bit = Convert.ToInt32(this.m_WebServiceAccount["EnableReportBrowser"].ToString());
                if(bit == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string AuthenticatorToken
        {
            get { return this.m_AuthenticatorToken; }
            set { this.m_AuthenticatorToken = value; }
        }

        public bool IsAuthenticated
        {
            get { return this.m_IsAuthenticated; }
            set { this.m_IsAuthenticated = value; }
        }

        public string GetPayloadField(string fieldName)
        {
            if (this.m_Token != null)
            {
                return this.m_Token.Payload[fieldName].ToString();
            }
            else
            {
                return null;
            }
        }

        public void SetupTestUser()
        {
            this.m_IsAuthenticated = true;
            this.m_UserName = "Amanda.Turner";
            this.m_Password = "asdfadsf";
        }        
    }
}
