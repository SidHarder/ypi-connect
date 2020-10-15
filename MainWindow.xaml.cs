using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YPIConnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthenticatedUser m_AuthenticatedUser;

        private TabItem m_TabItemReportBrowser;
        private ReportBrowserWorkspace m_ReportBrowserWorkspace;

        private TabItem m_TabItemOrderBrowser;
        private OrderBrowserWorkspace m_OrderBrowserWorkspace;

        public MainWindow()
        {            
            this.m_TabItemReportBrowser = new TabItem();
            this.m_TabItemReportBrowser.Header = "Report Browser";
            this.m_TabItemReportBrowser.Tag = "Order_Log";

            this.m_TabItemOrderBrowser = new TabItem();
            this.m_TabItemOrderBrowser.Header = "Order Browser";
            this.m_TabItemOrderBrowser.Tag = "Order_Browser";

            InitializeComponent();            
            this.Loaded += MainWindow_Loaded;
        }        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(LocalSettings.AreLocalSettingsConfigured() == true)
            {
                if(LocalSettings.Instance.SkipAuthentication == true)
                {
                    AuthenticatedUser.Instance.SetupTestUser();
                    this.ShowReportBrowserWorkspace();
                }
                else
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Owner = this;
                    loginWindow.ShowDialog();

                    this.m_AuthenticatedUser = AuthenticatedUser.Instance;
                    if (this.m_AuthenticatedUser.IsAuthenticated == true)
                    {                        
                        if (this.m_AuthenticatedUser.EnableReportBrowser == true)
                        {
                            this.ShowReportBrowserWorkspace();
                            this.ShowOrderBrowserWorkspace();
                            this.m_TabItemReportBrowser.Focus();
                        }
                    }
                }                
            }            
        }

        private void ShowReportBrowserWorkspace()
        {
            if (this.m_TabItemReportBrowser.Parent != null)
            {
                m_TabItemReportBrowser.Focus();
            }
            else
            {
                this.m_ReportBrowserWorkspace = new ReportBrowserWorkspace(this.TabControlMainWorkspace);
                this.m_TabItemReportBrowser.Content = this.m_ReportBrowserWorkspace;
                this.TabControlMainWorkspace.Items.Add(this.m_TabItemReportBrowser);
                this.m_TabItemReportBrowser.Focus();
            }
        }

        private void ShowOrderBrowserWorkspace()
        {
            if (this.m_TabItemOrderBrowser.Parent != null)
            {
                m_TabItemOrderBrowser.Focus();
            }
            else
            {
                this.m_OrderBrowserWorkspace = new OrderBrowserWorkspace(this.TabControlMainWorkspace);
                this.m_TabItemOrderBrowser.Content = this.m_OrderBrowserWorkspace;
                this.TabControlMainWorkspace.Items.Add(this.m_TabItemOrderBrowser);
                this.m_TabItemOrderBrowser.Focus();
            }
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            settingsDialog.ShowDialog();
        }        
    }
}
