using System;
using System.Collections.Generic;
using System.Text;

namespace YPIConnect
{
    public class AuthenticatedUser
    {
        private static AuthenticatedUser m_Instance;

        private string m_UserName;
        private string m_Password;
        private string m_AuthenticatorToken;
        private bool m_IsAuthenticated;

        private AuthenticatedUser()
        {

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
    }
}
