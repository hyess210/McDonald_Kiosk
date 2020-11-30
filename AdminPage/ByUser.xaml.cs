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
using LiveCharts;
using LiveCharts.Wpf;

namespace McDonald_Kiosk.AdminPage
{
    public partial class ByUser : Page
    {
        public SeriesCollection TopChart { get; set; }
        public SeriesCollection BottomChart { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public List<Menu> MenuData = new List<Menu>();

        public struct Menu
        {
            public int user_idx;
            public string user_name;
            public List<int> order_idx;
            public List<int> menu_idx;
            public List<string> menu_name;
            public List<int> total;
        };

        public ByUser()
        {
            InitializeComponent();
            CreateValues();
            GetValues();
        }

        private void GetValues()
        {
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM user";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Menu menu = new Menu();
                    menu.user_idx = int.Parse(rdr["user_idx"].ToString());
                    menu.user_name = rdr["user_name"].ToString();
                    menu.menu_idx = new List<int>();
                    menu.order_idx = new List<int>();
                    menu.menu_name = new List<string>();
                    menu.total = new List<int>();
                    MenuData.Add(menu);
                }
                rdr.Close();
            }
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM ordering";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int cnt = 0;
                    foreach(Menu menu in MenuData)
                    {
                        if(menu.user_idx.Equals(rdr["user_idx"].ToString()))
                        {
                            Menu tempMenu = new Menu();
                            tempMenu = MenuData[cnt];
                            tempMenu.order_idx.Add(int.Parse(rdr["order_idx"].ToString()));
                            MenuData[cnt] = tempMenu;
                        }
                        cnt++;
                    }
                }
                rdr.Close();
            }
        }

        private void CreateValues()
        {
            TopChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "회원별",
                    Values = new ChartValues<double> { }
                }
            };

            BottomChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "갯수",
                    Values = new ChartValues<double> { }
                }
            };

            SetValueType();
        }

        private void SetValueType()
        {

        }
    }
}
