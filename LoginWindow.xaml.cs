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
            this.DataContext = this;
        }

        public AuthenticatedUser AuthenticatedUser
        {
            get { return this.m_AuthenticatedUser; }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {            
            if (string.IsNullOrEmpty(this.m_AuthenticatedUser.UserName) == false &&
                string.IsNullOrEmpty(this.m_AuthenticatedUser.Password) == false &&
                string.IsNullOrEmpty(this.m_AuthenticatedUser.AuthenticatorToken) == false)
            {
                JObject apiRequest = APIRequestHelper.GetTokenMessage(this.m_AuthenticatedUser.UserName, this.m_AuthenticatedUser.Password, this.m_AuthenticatedUser.AuthenticatorToken);
                JObject apiResponse = APIRequestHelper.SubmitAPIRequestMessage(apiRequest);

                string tkn = apiResponse["result"]["token"].ToString();

                JwtSecurityToken token = new JwtSecurityToken(tkn);
                JwtPayload payload = token.Payload;

                LocalSettings.Instance.UpdateUserNamePassword(m_AuthenticatedUser);
                LocalSettings.Instance.Serialize();

                //alxw6JEp
            }
            else
            {
                MessageBox.Show("The Username and Password cannot be blank.");
            }
        }
    }
}
