using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        }

        private void SendMessage()
        {
            TcpClient tcp = new TcpClient("10.80.162.151", 80);
            if(!tcp.Connected)
            {
                MessageBox.Show("연결이 안되요!");
            }
            var json = new JObject();
            json.Add("MSGType", 2);
            json.Add("Id", "2211");
            json.Add("ShopName", "맥도날드");
            json.Add("OrderNumber", OrderNumOp(Customer.getInstance().order_idx));
            var menus = new JArray();
            int max = OrderState.GetInstance().Count;
            for (int i = 0; i < max; i++)
            {
                OrderState orderMenus = OrderState.GetInstance()[i];
                Console.WriteLine(orderMenus);
                var menu = new JObject();
                Console.WriteLine(orderMenus.Menu);
                menu.Add("Name", orderMenus.Menu);
                menu.Add("Count", orderMenus.Amount);
                menu.Add("Price", orderMenus.Price);
                Console.WriteLine(menu.ToString());
                menus.Add(menu);
            }
            Console.WriteLine(menus.ToString());
            json.Add("Menus", menus);
            byte[] buff = Encoding.UTF8.GetBytes(json.ToString());
            NetworkStream network = tcp.GetStream();
            network.Write(buff, 0, buff.Length);
            Console.WriteLine(json.ToString());
        }

        private string OrderNumOp(int num)
        {
            if (num > 100)
            {
                if (num % 100 == 0)
                    return "100";
                else 
                    return (num % 100).ToString();
            }
            else if (num >= 10)
                return "0" + num;
            else
                return "00" + num;
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
            SendMessage();
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
