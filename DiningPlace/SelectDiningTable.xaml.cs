using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace McDonald_Kiosk
{
    /// <summary>
    /// SelectDiningTable.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectDiningTable : Page
    {
        List<Table> tables = new List<Table>();
        List<Label> timeTexts = new List<Label>();
        public SelectDiningTable()
        {
            InitializeComponent();

            setTable();
            setTimeText();
            DBConnection();
            timerManage();
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
            timeTexts.Add(timer1);
            timeTexts.Add(timer2);
            timeTexts.Add(timer3);
            timeTexts.Add(timer4);
            timeTexts.Add(timer5);
            timeTexts.Add(timer6);
            timeTexts.Add(timer7);
            timeTexts.Add(timer8);
            timeTexts.Add(timer9);
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
                string sql = "SELECT order_time FROM ordering WHERE tableNum = " + count + " order by desc;";
                command = new MySqlCommand(sql, connection);
                dataReader = command.ExecuteReader();

                connection.Open();

                if (dataReader.IsDBNull(0))
                    tables[count].isEnabled = true;
                else
                {
                    DateTime order_time = (DateTime)dataReader[0];

                    TimeSpan leftTime = DateTime.Now - order_time;
                    if (TimeSpan.Compare(leftTime, new TimeSpan(0, 1, 0)) == 1)
                        tables[count].left_time = leftTime.Seconds;
                }

                connection.Close();
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
            for (int i = 0; i < 9; i++)
            {
                if (!tables[i].isEnabled)
                {
                    if (tables[i].left_time < 1)
                        tables[i].isEnabled = true;
                    else
                        --tables[i].left_time;
                }
                realTimeMapping(i, tables[i].isEnabled);
            }
            timer.Stop();
        }

        private void realTimeMapping(int idx, bool isEnabled)
        {
            if (isEnabled)
                timeTexts[idx].Content = "";
            else
                timeTexts[idx].Content = tables[idx].left_time;
        }

        private void Click_Grid(object sender, EventArgs args)
        {
            Grid param = sender as Grid;
            int idx = int.Parse(param.Name.Substring(5));
            if(tables[idx].isEnabled)
                param.Background = new SolidColorBrush(Color.FromRgb(255, 192, 0));
        }
    }
}