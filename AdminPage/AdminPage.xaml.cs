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

namespace McDonald_Kiosk.AdminPage
{
    /// <summary>
    /// AdminPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void GoByMenu_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "AdminPage/ByMenu.xaml");
        }

        private void GoSales_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "Sales.xaml");
        }

        private void GoNavigation_Click(object sender, RoutedEventArgs e, string navigateUrl)
        {
            NavigationService.Navigate(new Uri(navigateUrl, UriKind.Relative));
        }
    }
}
