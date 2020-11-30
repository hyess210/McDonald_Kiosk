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
    /// StatisticsPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string source = "AdminPage/ByMenu.xaml";
            ChangeSource(source);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string source = "AdminPage/ByDays.xaml";
            ChangeSource(source);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string source = "AdminPage/ByUser.xaml";
            ChangeSource(source);
        }

        private void ChangeSource(string value)
        {
            string source = value;
            Uri uri = new Uri(source, UriKind.Relative);
            ChartType.NavigationService.Navigate(uri);
        }
    }
}
