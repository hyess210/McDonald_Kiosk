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
        public SeriesCollection TopChart { get; set; }
        public SeriesCollection BottomChart { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public struct Menu_Info
        {
            public string name;
            public string category;
            public double price;
            public double total_count;
            public double total_price;
            public double[] count_table;
            public double[] price_table;
        };
        
        public struct Category_Info
        {
            public string name;
            public double total_count;
            public double total_price;
            public double[] count_table;
            public double[] price_table;
        };

        public List<Menu_Info> MenuData = new List<Menu_Info>();
        public List<Category_Info> CategoryData = new List<Category_Info>();

        public int minRange = 0, maxRange = 10;
        public bool SeatType = false, MenuType = true;

        public ByMenu()
        {
            InitializeComponent();
            ListSetting();
            GetValues();
            SetMenuLabel();
            CreateValues();
        }

        private void ListSetting()
        {
            for(int i = 0; i < 3; i++)
            {
                Category_Info ci = new Category_Info();
                double[] ctAmount = new double[9];
                double[] ctPrice = new double[9];
                ci.count_table = ctAmount;
                ci.price_table = ctPrice;
                CategoryData.Add(ci);
            }
        }

        private void GetValues()
        {//10.80.162.193
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";

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
                    menu.count_table = new double[9];
                    menu.price_table = new double[9];
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
                    double[] countArr = new double[9];
                    double[] priceArr = new double[9];
                    int menu_idx = int.Parse(rdr["menu_idx"].ToString()) - 1;
                    int tableNum = int.Parse(rdr["tableNum"].ToString()) - 1;

                    Menu_Info tempMenu = new Menu_Info();
                    tempMenu.name = MenuData[menu_idx].name;
                    tempMenu.category = MenuData[menu_idx].category;
                    tempMenu.price = MenuData[menu_idx].price;
                    tempMenu.total_count = MenuData[menu_idx].total_count + double.Parse(rdr["amount"].ToString());
                    tempMenu.total_price = tempMenu.total_count * MenuData[menu_idx].price;

                    if (MenuData[menu_idx].count_table != null)
                    {
                        countArr = MenuData[menu_idx].count_table;
                        priceArr = MenuData[menu_idx].price_table;
                    }

                    if (tableNum >= 0)
                    {
                        countArr[tableNum] += int.Parse(rdr["amount"].ToString());
                        priceArr[tableNum] = countArr[tableNum] * tempMenu.price;
                        tempMenu.count_table = countArr;
                        tempMenu.price_table = priceArr;
                    }
                    else
                    {
                        tempMenu.count_table = countArr;
                        tempMenu.price_table = priceArr;
                    }
                    MenuData[menu_idx] = tempMenu;

                    Category_Info category = new Category_Info();
                    double[] ctAmount = new double[9];
                    double[] ctPrice = new double[9];

                    if (tableNum >= 0)
                    {
                        ctAmount = CategoryData[GetTypeOfCategory(tempMenu.category)].count_table;
                        ctPrice = CategoryData[GetTypeOfCategory(tempMenu.category)].price_table;
                        ctAmount[tableNum] += double.Parse(rdr["amount"].ToString());
                        ctPrice[tableNum] = ctAmount[tableNum] * tempMenu.price;
                        category.count_table = ctAmount;
                        category.price_table = ctPrice;
                        CategoryData[GetTypeOfCategory(tempMenu.category)] = category;
                    }
                }

                foreach (Menu_Info menu in MenuData)
                {
                    int categoryType = GetTypeOfCategory(menu.category);

                    Category_Info category = new Category_Info();
                    category.name = menu.category;
                    category.total_count = menu.total_count + CategoryData[categoryType].total_count;
                    category.total_price = menu.total_price + CategoryData[categoryType].total_price;
                    category.count_table = CategoryData[categoryType].count_table;
                    category.price_table = CategoryData[categoryType].price_table;
                    CategoryData[categoryType] = category;
                }
                rdr.Close();
            }
        }

        private int GetTypeOfCategory(string value)
        {
            int categoryType = 0;

            if (value.Equals("burger"))
                categoryType = 0;
            else if (value.Equals("side"))
                categoryType = 1;
            else
                categoryType = 2;

            return categoryType;
        }

        private void CreateValues()
        {
            TopChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "갯수",
                    Values = new ChartValues<double> { }
                }
            };

            BottomChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "총액",
                    Values = new ChartValues<double> { }
                }
            };

            SetValueType();
        }

        private void SetMenuLabel()
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
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        private void SetCategoryLabel()
        {
            int cnt = 0;

            if (Labels == null)
                Labels = new string[MenuData.Count];
            else
            {
                for (int i = 0; i < MenuData.Count; i++)
                {
                    Labels[i] = null;
                }
            }
            foreach (Category_Info category in CategoryData)
            {
                Labels[cnt++] = category.name;
            }
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        private void SetValueType()
        {
            TopChart[0].Values.Clear();
            BottomChart[0].Values.Clear();

            if (!SeatType && MenuType)
            {
                SetMenuValues();
            }
            if (!SeatType && !MenuType)
            {
                SetCategoryValues();
            }
            if(SeatType && MenuType)
            {
                SetTableMenuValues();
            }
            if(SeatType && !MenuType)
            {
                SetTableCategoryValues();
            }
        }

        private void SetMenuValues()
        {
            int barCnt = 0;
            foreach (Menu_Info menu in MenuData)
            {
                if (barCnt >= minRange && barCnt < maxRange)
                {
                    TopChart[0].Values.Add(menu.total_count);
                    BottomChart[0].Values.Add(menu.total_price);
                }
                barCnt++;
            }
            SetMenuLabel();
        }

        private void SetCategoryValues()
        {
            int barCnt = 0;
            foreach (Category_Info category in CategoryData)
            {
                if (barCnt < 3)
                {
                    TopChart[0].Values.Add(category.total_count);
                    BottomChart[0].Values.Add(category.total_price);
                }
                barCnt++;
            }
            SetCategoryLabel();
        }

        private void SetTableMenuValues()
        {
            int barCnt = 0;
            int tableNum = Seat.SelectedIndex - 1;
            foreach (Menu_Info menu in MenuData)
            {
                if (barCnt >= minRange && barCnt < maxRange)
                {
                    TopChart[0].Values.Add(double.Parse(menu.count_table[tableNum].ToString()));
                    BottomChart[0].Values.Add(double.Parse(menu.price_table[tableNum].ToString()));
                }
                barCnt++;
            }
            SetMenuLabel();
        }

        private void SetTableCategoryValues()
        {
            double[] count = new double[3];
            double[] price = new double[3];
            
            foreach (Category_Info category in CategoryData)
            {
                TopChart[0].Values.Add(category.count_table[Seat.SelectedIndex - 1]);
                BottomChart[0].Values.Add(category.price_table[Seat.SelectedIndex - 1]);
            }
            SetCategoryLabel();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.SelectedIndex = 0;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            Menu_Category.IsEnabled = true;

            if (cb.SelectedIndex == 0)
            {
                SeatType = false;
            }
            else if(cb.SelectedIndex != 0 && Menu_Category.SelectedIndex == 1)
            {
                Page.SelectedIndex = 4;
                Page.IsEnabled = false;
                SeatType = true;
            }
            else if(cb.SelectedIndex != 0 && Menu_Category.SelectedIndex == 0)
            {
                Page.SelectedIndex = 0;
                Page.IsEnabled = true;
                SeatType = true;
            }

            SetValueType();
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (cb.SelectedIndex == 0)
            {
                Page.SelectedIndex = 0;
                Page.IsEnabled = true;
                MenuType = true;
            }
            else
            {
                MenuType = false;
                Page.SelectedIndex = 4;
                minRange = 0;
                maxRange = 10;
                Page.IsEnabled = false;
            }

            SetValueType();
        }

        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (MenuType && cb.SelectedIndex == 4)
                cb.SelectedIndex = 3;

            minRange = cb.SelectedIndex * 10;
            maxRange = (cb.SelectedIndex + 1) * 10;

            SetValueType();
        }
    }
}
