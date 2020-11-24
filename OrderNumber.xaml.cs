using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace McDonald_Kiosk
{
    /// <summary>
    /// OrderNumber.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderNumber : Page
    {
        DB mysqlDB = new Mysql();
        DispatcherTimer timer = new DispatcherTimer();

        public OrderNumber()
        {
            InitializeComponent();
            SendMessage();
        }

        private void SendMessage()
        {
            TcpClient tcp = new TcpClient("10.80.163.155", 80);
            string msg = "{" +
                         "  \"MSGType\" : 2" +
                         "  \"Id\" : " + Customer.getInstance().user_id;
                         "  \"\""
        }

        public void Label_Loaded(object sender, RoutedEventArgs e)
        {
            string connStr = "Server=localhost;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            int orderNum = 0;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM ordering";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    orderNum = int.Parse(rdr["order_idx"].ToString());
                }
                rdr.Close();
            }

            Customer.getInstance().order_idx = orderNum + 1;
            Label label = (Label)sender;
            label.Content = "주문번호 : " + Customer.getInstance().order_idx;

            InsertAllData();
            DataReset();
            RestartCount();
        }

        private void InsertAllData()
        {
            var customer = Customer.getInstance();
            var order = OrderState.GetInstance();
            mysqlDB.InsertData(customer.order_idx, customer.user_idx, customer.tableNum, customer.isCard);

            for(int i = 0; i < order.Count; i++)
            {
                mysqlDB.InsertData(order[i].Menu_idx, customer.order_idx, order[i].Amount);
            }
        }

        private void DataReset()
        {
            OrderState.GetInstance().Clear();
        }

        private void RestartCount()
        {
            timer.Interval = TimeSpan.FromSeconds(15);
            timer.Tick += GoHomePage;
            timer.Start();
        }

        private void GoHomePage(object sender, EventArgs e)
        {
            Home home = new Home();
            NavigationService.Navigate(home);
            timer.Stop();
        }

        public void Label1_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            float totalPayment = 0;

            for(int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                totalPayment += OrderState.GetInstance()[i].Total;
            }
            label.Content = "총 결제 금액 : " + totalPayment;
        }

        public void Label2_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            label.Content = "회원명 : " + Customer.getInstance().user_name;
        }

        public void Label3_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            label.Content = "카드번호 : " + Customer.getInstance().user_barcode;
        }
    }
}
