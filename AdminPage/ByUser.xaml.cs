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
        public string[] BotLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public List<Menu> MenuData = new List<Menu>();

        public struct Menu
        {
            public int user_idx;
            public string user_name;
            public List<int> order_idx;
            public List<int> menu_idx;
            public List<string> menu_name;
            public List<int> menu_amount;
            public List<int> total;
        };

        public ByUser()
        {
            BotLabels = new string[10];

            InitializeComponent();
            CreateValues();
            GetValues();
            SetTopValue();
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
                    menu.menu_amount = new List<int>();
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
                    for(int i = 0; i < MenuData.Count; i++)
                    {
                        if(MenuData[i].user_idx.ToString().Equals(rdr["user_idx"].ToString()))
                        {
                            Menu tempMenu = new Menu();
                            tempMenu = MenuData[i];
                            tempMenu.order_idx.Add(int.Parse(rdr["order_idx"].ToString()));
                            MenuData[i] = tempMenu;
                        }
                    }
                }
                rdr.Close();
            }
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM ordered_menu";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for(int i = 0; i < MenuData.Count; i++)
                    {
                        Console.WriteLine(MenuData[i].order_idx.ToString());
                        for(int j = 0; j < MenuData[i].order_idx.Count; j++)
                        {
                            if (MenuData[i].order_idx[j].ToString().Equals(rdr["order_idx"].ToString()))
                            {
                                Console.WriteLine("%");
                                Menu tempMenu = new Menu();
                                tempMenu = MenuData[i];
                                tempMenu.menu_idx.Add(rdr.GetInt32("menu_Idx"));
                                tempMenu.menu_amount.Add(int.Parse(rdr["amount"].ToString()));
                                tempMenu.total.Add(int.Parse(rdr["total"].ToString()));
                                MenuData[i] = tempMenu;
                            }
                        }
                    }
                }
                rdr.Close();
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM menu";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for(int i = 0; i < MenuData.Count; i++)
                    {
                        for(int j = 0; j < MenuData[i].menu_idx.Count; j++)
                        {
                            if(MenuData[i].menu_idx[j].ToString().Equals(rdr["menu_idx"]))
                            {
                                Menu menu = new Menu();
                                menu = MenuData[i];
                                menu.menu_name.Add(rdr["menu_name"].ToString());
                                MenuData[i] = menu;
                            }
                        }
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
                    Title = "총액",
                    Values = new ChartValues<double> { }
                }
            };

            BottomChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "사용자별 갯수",
                    Values = new ChartValues<double> { }
                }
            };
        }

        private void SetTopValue()
        {
            Labels = new string[MenuData.Count];
            for (int i = 0; i < MenuData.Count; i++)
            {
                double total = 0;
                for (int j = 0; j < MenuData[i].total.Count; j++)
                {
                    total += int.Parse(MenuData[i].total[j].ToString());
                }
                Console.WriteLine(total + " " + MenuData[i].user_name);
                TopChart[0].Values.Add(total);
                Labels[i] = MenuData[i].user_name;
            }
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        private void SetUserAmount(string name)
        {
            for (int i = 0; i < MenuData.Count; i++)
            {
                if(MenuData[i].user_name.Equals(name))
                {

                    for (int j = 0; j < MenuData[i].menu_amount.Count; j++)
                    {
                        BottomChart[0].Values.Add(double.Parse(MenuData[i].menu_amount[j].ToString()));
                    }
                    for(int h = 0; h < 10; h++)
                    {
                        if(h <= MenuData[i].menu_name.Count - 1)
                            BotLabels[h] = MenuData[i].menu_name[h];
                    }
                }
            }
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BottomChart[0].Values.Clear();
            SetUserAmount(combo.Text.ToString());
            Console.WriteLine(combo.Text.ToString());
        }
    }
}