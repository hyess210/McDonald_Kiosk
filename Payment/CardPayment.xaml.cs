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
using MySql.Data.MySqlClient;

namespace McDonald_Kiosk
{
    public partial class CardPayment : Page
    {
        public CardPayment()
        {
            InitializeComponent();
            webcam.CameraIndex = 0;
        }

        private void webcam_QrDecoded(object sender, string e) 
        {
            bool isRegistered = false;
            string connStr = "Server=localhost;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM user";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    if(e.Equals(rdr["User_name"]))
                    {
                        isRegistered = true;
                        Customer.getInstance().user_idx = int.Parse(rdr["user_idx"].ToString());
                        Customer.getInstance().user_name = rdr["user_name"].ToString();
                        Customer.getInstance().user_barcode = rdr["barcode"].ToString();
                        Customer.getInstance().isCard = true;
                        break;
                    }
                }
                rdr.Close();
            }

            if(isRegistered)
            {
                OrderNumber orderNumber = new OrderNumber();
                NavigationService.Navigate(orderNumber);
            }
            else
            {
                tbRecog.Content = "가입되지 않은 바코드입니다.";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            float totalPayment = 0;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                totalPayment += OrderState.GetInstance()[i].Total;
            }
            label.Content = "총 결제 금액 : " + totalPayment;
        }
    }
}
