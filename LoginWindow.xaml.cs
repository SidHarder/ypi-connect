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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace YPIConnect
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AuthenticatedUser m_AuthenticatedUser;

        public LoginWindow()
        {
            this.m_AuthenticatedUser = AuthenticatedUser.Instance;
            InitializeComponent();
            this.Loaded += LoginWindow_Loaded;
            this.DataContext = this;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.PasswordBoxPassword.Password = this.m_AuthenticatedUser.Password;
            this.TextBoxAuthenticatorToken.Focus();
        }

        public AuthenticatedUser AuthenticatedUser
        {
            get { return this.m_AuthenticatedUser; }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            this.DoAuthenticateUser();
        }

        private void DoAuthenticateUser()
        {
            if (string.IsNullOrEmpty(this.PasswordBoxPassword.Password) == false &&
                string.IsNullOrEmpty(this.m_AuthenticatedUser.Password) == false &&
                string.IsNullOrEmpty(this.m_AuthenticatedUser.AuthenticatorToken) == false)
            {
                JObject apiRequest = APIRequestHelper.GetTokenMessage(this.m_AuthenticatedUser.UserName, this.PasswordBoxPassword.Password, this.m_AuthenticatedUser.AuthenticatorToken);
                APIResult apiResponse = APIRequestHelper.SubmitAPIRequestMessage(apiRequest);
                if (Convert.ToBoolean(apiResponse.JSONResult["result"]["isAuthenticated"].ToString()) == true)
                {
                    string tkn = apiResponse.JSONResult["result"]["token"].ToString();

                    this.m_AuthenticatedUser.WebServiceAccount = (JObject)apiResponse.JSONResult["result"]["webServiceAccount"];
                    this.m_AuthenticatedUser.WebServiceAccountClient = (JArray)apiResponse.JSONResult["result"]["webServiceAccountClient"];

                    JwtSecurityToken token = new JwtSecurityToken(tkn);
                    this.m_AuthenticatedUser.IsAuthenticated = true;
                    this.m_AuthenticatedUser.Token = token;

                    LocalSettings.Instance.UpdateUserNamePassword(m_AuthenticatedUser);
                    LocalSettings.Instance.Serialize();

                    this.Close();
                }
                else
                {
                    this.m_AuthenticatedUser.IsAuthenticated = true;
                    MessageBox.Show("The Authenticator Token provided is not valid.");
                }
            }
            else
            {
                MessageBox.Show("The Username and Password cannot be blank.");
            }
        }        

        private void TextBoxAuthenticatorToken_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.DoAuthenticateUser();
            }
        }
    }
}
//alxw6JEp
