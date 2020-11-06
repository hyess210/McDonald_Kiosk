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
                    //Console.WriteLine("{0} : {1} : {2} : {3} : {4}", rdr["user_idx"], rdr["user_id"], rdr["user_password"], rdr["user_name"], rdr["barcode"]);
                    if(e.Equals(rdr["User_name"]))
                    {
                        Console.WriteLine("Logined");
                        isRegistered = true;
                        break;
                    }
                }
                rdr.Close();
            }

            if(isRegistered)
            {
                tbRecog.Content = "회원명 : " + e;
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
