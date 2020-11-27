using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace McDonald_Kiosk
{
    /// <summary>
    /// SelectDiningTable.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectDiningTable : Page
    {
        List<Table> tables = new List<Table>();
        List<Grid> grids = new List<Grid>();
        List<Label> timeTexts = new List<Label>();
        int selectedIdx = -1;
        int beforeCount = 0;
        public SelectDiningTable()
        {
            InitializeComponent();

            setTable();
            setTimeText();
            setGrids();
            DBConnection();
            timerManage();
            changeBackground();
        }

        private void setTable()
        {
            Table table1 = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            Table table4 = new Table();
            Table table5 = new Table();
            Table table6 = new Table();
            Table table7 = new Table();
            Table table8 = new Table();
            Table table9 = new Table();

            tables.Add(table1);
            tables.Add(table2);
            tables.Add(table3);
            tables.Add(table4);
            tables.Add(table5);
            tables.Add(table6);
            tables.Add(table7);
            tables.Add(table8);
            tables.Add(table9);
        }

        private void setTimeText()
        {
            timeTexts.Add(timer0);
            timeTexts.Add(timer2);
            timeTexts.Add(timer3);
            timeTexts.Add(timer4);
            timeTexts.Add(timer5);
            timeTexts.Add(timer6);
            timeTexts.Add(timer7);
            timeTexts.Add(timer8);
            timeTexts.Add(timer9);
        }

        private void setGrids()
        {
            grids.Add(Table1);
            grids.Add(Table2);
            grids.Add(Table3);
            grids.Add(Table4);
            grids.Add(Table5);
            grids.Add(Table6);
            grids.Add(Table7);
            grids.Add(Table8);
            grids.Add(Table9);
        }

        private void DBConnection()
        {
            int count = 1;
            string url = "server=10.80.162.193; user=root; database=mcdonald_kiosk; port=3306; password=kmk5632980; sslmode=none;";
            MySqlConnection connection = new MySqlConnection(url);
            MySqlCommand command;
            MySqlDataReader dataReader;

            while (count < 10)
            {
                string sql = "SELECT order_time FROM ordering WHERE tableNum=" + count + " ORDER BY order_time DESC;";
                command = new MySqlCommand(sql, connection);
                connection.Open();
                dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                     DateTime order_time = (DateTime)dataReader[0];

                     TimeSpan leftTime = new TimeSpan(0, 1, 0) - (DateTime.Now - order_time);
                     if (TimeSpan.Compare(leftTime, new TimeSpan(0, 1, 0)) == -1 && TimeSpan.Compare(leftTime, new TimeSpan(0, 0, 0)) == 1)
                         tables[count - 1].left_time = leftTime.Seconds;
                     else
                         tables[count - 1].isEnabled = true;
                }
                else
                {
                    tables[count - 1].isEnabled = true;
                }
                connection.Close();
                count++;
            }
        }

        private void timerManage()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (object s, EventArgs a) => timer_Tick(s, a, timer);
            timer.Start();
        }

        private void timer_Tick(object s, EventArgs a, DispatcherTimer timer)
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                if (!tables[i].isEnabled)
                {
                    if (tables[i].left_time < 1)
                        tables[i].isEnabled = true;
                    else
                        --tables[i].left_time;
                    count++;
                }
                realTimeMapping(i, tables[i].isEnabled);
                if (count != beforeCount)
                {
                    changeBackground();
                    if (count == 9)
                        timer.Stop();
                }
            }
            
        }

        private void realTimeMapping(int idx, bool isEnabled)
        {
            if (isEnabled)
                timeTexts[idx].Content = "";
            else
                timeTexts[idx].Content = tables[idx].left_time;
        }

        private void changeBackground()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!tables[i].isEnabled)
                    grids[i].Background = new SolidColorBrush(Color.FromRgb(234, 234, 234));
                else
                    grids[i].Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void Click_Grid(object sender, EventArgs args)
        {
            Grid param = sender as Grid;
            selectedIdx = int.Parse(param.Tag.ToString());
            goToPay.IsEnabled = true;
        }

        private void goToPay_Click(object sender, EventArgs args)
        {
            Customer.getInstance().tableNum = selectedIdx;
            SelectPayment select = new SelectPayment();
            NavigationService.Navigate(select);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}