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
        public TotalPage()
        {
            InitializeComponent();

            int totalProfit = 203203;
            int noneSaleProfit = 10238;
            int saledPrice = 210293;

            tbTotal.Text = totalProfit.ToString();
            tbNoSaleTotal.Text = noneSaleProfit.ToString();
            tbSaledPrice.Text = saledPrice.ToString();

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

                    //menuList.Add(new OrderMenu.Food()
                    //{
                    //    Price = Int32.Parse(rdr["price"].ToString()),
                    //    Name = rdr["menu_name"].ToString(),
                    //    ImgPath = rdr["stored_path"].ToString(),
                    //    Menu_idx = Int32.Parse(rdr["menu_idx"].ToString())
                    //});

                    //string category = rdr["category"].ToString();

                    //if (category.Equals("burger"))
                    //{
                    //    menuList[i].category = Category.BUGER;
                    //}
                    //else if (category.Equals("side"))
                    //{
                    //    menuList[i].category = Category.SIDE;
                    //}
                    //else if (category.Equals("drink"))
                    //{
                    //    menuList[i].category = Category.DRINK;
                    //}
                    //i++;
                }
                rdr.Close();
                //lbMenus.ItemsSource = lstMenu;
            }
        }
    }
}
