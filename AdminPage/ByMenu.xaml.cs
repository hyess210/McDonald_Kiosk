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
    public partial class ByMenu : Page
    {
        //Binding_xaml
        public SeriesCollection CountChart { get; set; }
        public SeriesCollection AmountChart { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public struct Menu_Info
        {
            public string name;
            public string category;
            public double price;
            public double total_count;
            public double total_price;
        };

        public List<Menu_Info> MenuData = new List<Menu_Info>();
        public int minRange = 0, maxRange = 10;

        public ByMenu()
        {
            InitializeComponent();
            GetValues();
            SetLabel();
            CreateValues();
        }

        private void GetValues()
        {
            string connStr = "Server=localhost;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM menu";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Menu_Info menu = new Menu_Info();
                    menu.name = rdr["menu_name"].ToString();
                    menu.category = rdr["category"].ToString();
                    menu.price = double.Parse(rdr["price"].ToString());
                    MenuData.Add(menu);
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
                    int menu_idx = int.Parse(rdr["menu_idx"].ToString()) - 1;

                    Menu_Info tempMenu = new Menu_Info();
                    tempMenu.name = MenuData[menu_idx].name;
                    tempMenu.category = MenuData[menu_idx].category;
                    tempMenu.price = MenuData[menu_idx].price;
                    tempMenu.total_count = MenuData[menu_idx].total_count + double.Parse(rdr["amount"].ToString());
                    tempMenu.total_price = tempMenu.total_count * MenuData[menu_idx].price;

                    MenuData[menu_idx] = tempMenu;
                }
                rdr.Close();
            }
        }

        private void SetLabel()
        {
            int label = 0, cnt = 0;
            if(Labels == null)
                Labels = new string[MenuData.Count];
            else
            {
                for(int i = 0; i < MenuData.Count; i++)
                {
                    Labels[i] = null;
                }
            }

            foreach(Menu_Info menu in MenuData)
            {
                if(cnt >= minRange && cnt < maxRange)
                {
                    Labels[label++] = menu.name;
                }
                cnt++;
            }
        }

        private void CreateValues()
        {
            CountChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "갯수",
                    Values = new ChartValues<double> { }
                }
            };

            AmountChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "총액",
                    Values = new ChartValues<double> { }
                }
            };

            SetValues();
        }

        private void SetValues()
        {
            int barCnt = 0;
            CountChart[0].Values.Clear();
            AmountChart[0].Values.Clear();

            foreach (Menu_Info menu in MenuData)
            {
                if (barCnt >= minRange && barCnt < maxRange)
                {
                    CountChart[0].Values.Add(menu.total_count);
                    AmountChart[0].Values.Add(menu.total_price);
                }
                barCnt++;
            }

            barCnt = 0;
            Formatter = value => value.ToString("N");
            DataContext = this;
            SetLabel();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            switch(cb.SelectedIndex)
            {
                case 0:
                    minRange = 0;
                    maxRange = 10;
                    break;
                case 1:
                    minRange = 10;
                    maxRange = 20;
                    break;
                case 2:
                    minRange = 20;
                    maxRange = 30;
                    break;
                case 3:
                    minRange = 30;
                    maxRange = 40;
                    break;
            }
            //Console.WriteLine(minRange + " " + maxRange);
            SetValues();
        }
    }
}
