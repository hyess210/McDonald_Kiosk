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

namespace McDonald_Kiosk
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void playVideo()
        {
            VideoDrawing video = new VideoDrawing();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderMenuPage order = new OrderMenuPage();
            NavigationService.Navigate(order);
        }

        private void ShowLoginPage()
        {
            if(!Customer.getInstance().isAutoLogin && !Customer.getInstance().isLogin)
            {
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLoginPage();

        }

        private void GoAdminPage_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "AdminPage/AdminPage.xaml");
        }
        private void GoNavigation_Click(object sender, RoutedEventArgs e, string navigateUrl)
        {
            NavigationService.Navigate(new Uri(navigateUrl, UriKind.Relative));
        }
    }
}
