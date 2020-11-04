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
        SelectDiningPlace select = new SelectDiningPlace();
        List<Table> tables = new List<Table>();
        List<Label> labels = new List<Label>();
        public SelectDiningTable()
        {
            InitializeComponent();

            tableManage();
            timerManage();
            timeTextManage();
            DBConnection();
        }
        private void DBConnection()
        {
            string url = "server=10.80.162.193; user=root; database=mcdonald_kiosk; port=3306; password=kmk5632980; sslmode=none;";
            string sql = "SELECT order_time FROM ordering";
            MySqlConnection connection = new MySqlConnection(url);
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader dataReader;

            connection.Open();
            dataReader = command.ExecuteReader();
            int count = 0;

            while (dataReader.Read())
            {
                DateTime order_time = (DateTime)dataReader["order_time"];
                getLeftTime(order_time, count++);

            }
        }

        private void tableManage()
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

        private void timeTextManage()
        {
            labels.Add(timer1);
            labels.Add(timer2);
            labels.Add(timer3);
            labels.Add(timer4);
            labels.Add(timer5);
            labels.Add(timer6);
            labels.Add(timer7);
            labels.Add(timer8);
            labels.Add(timer9);
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
                }
                else
                    ++count;
                leftTimeMapping(i);
            }
            if (count == 9)
                timer.Stop();

        }

        private void leftTimeMapping(int idx)
        {
            if (tables[idx].isEnabled)
                timeText[idx].Content = "";
            else
                timeText[idx].Content = tables[idx].left_time;
        }

        

        private void getLeftTime(DateTime order_time, int count)
        {
            if (leftTime < 60 || leftTime > 0)
            {
                timeText[count].Content = leftTime;
            }
            else
            {
                timeText[count].Content = "";
            }
        }
    }
}