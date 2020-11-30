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

            int totalProfit = 0;

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
                    totalProfit += Int32.Parse(rdr["total"].ToString());
                }
                rdr.Close();
                tbTotal.Text = totalProfit.ToString();
            }
        }
    }
}
