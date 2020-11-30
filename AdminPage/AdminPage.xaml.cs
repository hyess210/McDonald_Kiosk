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
        int hour = 0;
        int min = 0;
        int sec = 0;

        string userName = "";
        string userQR = "";
        string userBarcode = "";

        public AdminPage()
        {
            InitializeComponent();

            RunningTimeManage();
            GetUserInfo();
        }

        public void RunningTimeManage()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (object s, EventArgs a) => RunningTimer(s, a, timer);
            timer.Start();
        }

        private void RunningTimer(object sender, EventArgs args, DispatcherTimer timer)
        {
            void timeManage(double time, TextBlock textBlock)
            {
                if (time < 10)
                {
                    textBlock.Text = "0" + time.ToString();
                }
                else
                {
                    textBlock.Text = time.ToString();
                }

            }

            Properties.Settings.Default.runningTime++;
            Properties.Settings.Default.Save();

            TimeSpan timespan = TimeSpan.FromSeconds(Properties.Settings.Default.runningTime);

            hour = timespan.Hours;
            min = timespan.Minutes;
            sec = timespan.Seconds;

            timeManage(hour, tbRunningTimeHour);
            timeManage(sec, tbRunningTimeSecond);
            timeManage(min, tbRunningTimeMinute);
            timeManage(sec, tbRunningTimeSecond);
        }

        private void GetUserInfo()
        {
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
                string sql = "SELECT * FROM user";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    userName = rdr["user_name"].ToString();
                    userQR = rdr["user_id"].ToString();
                    userBarcode = rdr["barcode"].ToString();
                }
                rdr.Close();
                lvUserInfo.Items.Refresh();
            }
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
