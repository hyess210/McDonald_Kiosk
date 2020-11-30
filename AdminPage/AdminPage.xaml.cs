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
using MySql.Data.MySqlClient;
using System.Windows.Navigation;
using System.Windows.Threading;
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

            runningTimeManage();
        }

        public void runningTimeManage()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (object s, EventArgs a) => runningTimer(s, a, timer);
            timer.Start();
        }

        //private void MainWindow_Exit(object sender, ExitEventArgs e)
        //{
        //    Properties.Settings.Default.Save();
        //}

        private void runningTimer(object sender, EventArgs args, DispatcherTimer timer)
        {
            void timeManage(double time, TextBlock textBlock)
            {
                if (time > 59)
                {
                    textBlock.Text = "0";
                }
                else
                {
                    textBlock.Text = time.ToString();
                }

            }

            Properties.Settings.Default.runningTime++;
            Properties.Settings.Default.Save();

            timeManage(Math.Floor((double)(Properties.Settings.Default.runningTime / 3600)), tbRunningTimeHour);
            tbRunningTimeSecond.Text = Math.Floor((double)Properties.Settings.Default.runningTime % 3600).ToString();
            timeManage(Math.Floor((double)(Properties.Settings.Default.runningTime / 60)), tbRunningTimeMinute);
            tbRunningTimeSecond.Text = Math.Floor((double)Properties.Settings.Default.runningTime % 60).ToString();
            //timeManage(Math.Floor((double)Properties.Settings.Default.runningTime % 3600), tbRunningTimeSecond);
        }

        private void GoTotal_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "AdminPage/TotalPage.xaml");
        }

        private void GoByMenu_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "AdminPage/StatisticsPage.xaml");
        }

        private void GoSales_Click(object sender, RoutedEventArgs e)
        {
            GoNavigation_Click(sender, e, "AdminPage/Sales.xaml");
        }

        private void GoNavigation_Click(object sender, RoutedEventArgs e, string navigateUrl)
        {
            NavigationService.Navigate(new Uri(navigateUrl, UriKind.Relative));
        }
    }
}
