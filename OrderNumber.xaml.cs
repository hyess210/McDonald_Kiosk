using MySql.Data.MySqlClient;
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
using System.Windows.Threading;

namespace McDonald_Kiosk
{
    /// <summary>
    /// OrderNumber.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderNumber : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public OrderNumber()
        {
            InitializeComponent();
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

            InsertOrdering();
            InsertOrderedMenu();
            DataReset();

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

        private void DataReset()
        {
            OrderState.GetInstance().Clear();
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

        public void InsertOrdering()
        {
            string connStr = "Server=localhost;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            MySqlConnection connection = new MySqlConnection(connStr);
            
            string sql = "Insert INTO ordering(order_idx, user_idx, tableNum, isCard) VALUES (" 
                + Customer.getInstance().order_idx + ',' 
                + Customer.getInstance().user_idx + ','
                + Customer.getInstance().tableNum + ',' 
                + Customer.getInstance().isCard + ")";

            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }

        public void InsertOrderedMenu()
        {
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            MySqlConnection connection = new MySqlConnection(connStr);
            //OrderState.GetInstance().Add(new OrderState() { Menu_idx = 1, Menu = "불고기 버거", Price = 3000, Amount = 2, Total = 6000 });
            string sql = "Insert INTO ordered_menu(menu_idx, order_idx, amount) VALUES ("
                + OrderState.GetInstance()[0].Menu_idx + ',' 
                + Customer.getInstance().order_idx + ','
                + OrderState.GetInstance()[0].Amount + ")";

            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
        }
    }
}
