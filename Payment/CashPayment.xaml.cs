using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class CashPayment : Page
    {
        public CashPayment()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void TextBox_Load(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(CardNumber);
        }

        private void Label1_Loaded(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            float totalPayment = 0;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                totalPayment += OrderState.GetInstance()[i].Total;
            }
            label.Content = "총 결제 금액 : " + totalPayment;
        }

        private void CardNumber_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
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
                    if (tb.Text.Equals(rdr["barcode"]))
                    {
                        Console.WriteLine("Logined");
                        isRegistered = true;
                        break;
                    }
                }
                rdr.Close();
            }

            if (isRegistered)
            {
                UserName.Content = "인증되었습니다.";
                OrderNumber orderNumber = new OrderNumber();
                NavigationService.Navigate(orderNumber);
            }
            else
            {
                UserName.Content = "가입되지 않은 바코드입니다.";
            }
        }
    }
}
