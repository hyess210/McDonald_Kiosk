using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
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
    public partial class TotalPage : Page
    {
        // isCard = 2 : 전체보기 / isCard = 0 : 현금 결제 매출액 / isCard = 1 : 카트 결제 매출액
        int isCard = 2;

        int totalProfit = 0;

        public TotalPage()
        {
            InitializeComponent();
            tbTotal.Text = totalProfit.ToString();
        }

        private void totalProfitValue()
        {
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
                string sql = "SELECT * FROM ordered_menu";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (isCard == 2)
                    {
                        totalProfit += Int32.Parse(rdr["total"].ToString());
                    }
                    else if (isCard == Int32.Parse(rdr["isCard"].ToString()))
                    {
                        totalProfit += Int32.Parse(rdr["total"].ToString());
                    }
                    else
                    {
                        totalProfit = 0;
                    }
                }

                if (tbTotal != null)
                {
                    tbTotal.Text = totalProfit.ToString();
                }

                rdr.Close();
            }
        }

        private void cbTotalProfit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbTotalProfit.SelectedIndex)
            {
                case 0:
                    isCard = 0;
                    break;
                case 1:
                    isCard = 1;
                    break;
                default:
                    isCard = 2;
                    break;
            }
            totalProfitValue();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
