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
    /// <summary>
    /// ByDays.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ByDays : Page
    {
        public SeriesCollection TopChart { get; set; }
        public SeriesCollection BottomChart { get; set; }   
        public string[] TopLabels { get; set; }
        public string[] BotLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public List<Day_Info> DayData = new List<Day_Info>();

        public List<Hour_Info> HourData = new List<Hour_Info>();

        public double[] PricePerTime = new double[3];

        public struct Day_Info
        {
            public double Selling;
            public DateTime date;
            public List<int> order_idx;
        };

        public struct Hour_Info
        {
            public string hour;
            public int orderNum;
            public int Selling;
        };

        public ByDays()
        {
            InitializeComponent();

            for (int i = 0; i < 7; i++)
            {
                Day_Info di = new Day_Info();
                di.order_idx = new List<int>();
                DayData.Add(di);
            }

            GetValue();
            SetChart();
        }

        private void GetValue()
        {
            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM ordering ORDER BY order_idx DESC";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                string tempDate = "temp";
                int dayCnt = -1;

                while (rdr.Read())
                {
                    if (dayCnt >= 6)
                        break;
                    
                    Day_Info tempDay = new Day_Info();
                    tempDay.order_idx = new List<int>();

                    if (((DateTime)rdr["order_time"]).Day.ToString().Equals(tempDate))
                    {
                        DayData[dayCnt].order_idx.Add(int.Parse(rdr["order_idx"].ToString()));
                    }
                    else
                    {
                        tempDate = ((DateTime)rdr["order_time"]).Day.ToString();
                        tempDay.date = (DateTime)rdr["order_time"];
                        tempDay.order_idx.Add(int.Parse(rdr["order_idx"].ToString()));
                        DayData[dayCnt + 1] = tempDay;
                        dayCnt++;
                    }

                    Hour_Info hourInfo = new Hour_Info();
                    hourInfo.hour = ((DateTime)rdr["order_time"]).Hour.ToString();
                    hourInfo.orderNum = int.Parse(rdr["order_idx"].ToString());
                    HourData.Add(hourInfo);
                }
                rdr.Close();
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT * FROM ordered_menu ORDER BY ordered_idx DESC";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                int cnt = 0;
                int payCnt = 0;
                string pastIdx = "temp";
                while (rdr.Read())
                {
                   Day_Info tempDI = new Day_Info();

                   if(rdr["order_idx"].ToString().Equals(pastIdx))
                   {
                        payCnt++;
                   }
                   else
                   {
                        Console.WriteLine("!");
                        payCnt = 0;
                        pastIdx = rdr["order_idx"].ToString();
                   }
                   for(int i = 0; i < DayData.Count; i++)
                   {
                        foreach (int idx in DayData[i].order_idx)
                        {
                            if (rdr["order_idx"].ToString().Equals(idx.ToString()))
                            {
                                tempDI = DayData[i];
                                tempDI.Selling += int.Parse(rdr["total"].ToString());
                                DayData[i] = tempDI;
                                cnt++;
                            }
                        }
                    }

                   for(int i = 0; i < HourData.Count; i++)
                   {
                        if (HourData[i].orderNum.ToString().Equals(rdr["order_idx"].ToString()))
                        {
                            int hour = int.Parse(HourData[i].hour);

                            Console.WriteLine(hour);
                            if( hour >= 6 && hour <= 12)
                            {
                                PricePerTime[0] += int.Parse(rdr["total"].ToString());
                                break;
                            }
                            else if(hour > 12 && hour <= 24)
                            {
                                PricePerTime[1] += int.Parse(rdr["total"].ToString());
                                break;
                            }
                            else
                            {
                                PricePerTime[2] += int.Parse(rdr["total"].ToString());
                                break;
                            }
                        }
                   }
                }
                rdr.Close();
            }
        }

        private void SetChart()
        {
            TopChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "일별",
                    Values = new ChartValues<double> { }
                }
            };

            BottomChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "시간대별",
                    Values = new ChartValues<double> { }
                }
            };
            SetValue();
        }

        private void SetValue()
        {
            TopLabels = new string[7];
            BotLabels = new string[3];

            for (int i = 0; i < DayData.Count; i++)
            {
                if (DayData[i].date.Year.ToString().Equals("1"))
                    break;

                TopChart[0].Values.Add(DayData[i].Selling);
                TopLabels[i] = DayData[i].date.Year.ToString()
                            + " " + DayData[i].date.Month.ToString()
                            + " " + DayData[i].date.Day.ToString();
            }

            for(int i = 0; i < 3; i++)
            {
                BottomChart[0].Values.Add(PricePerTime[i]);
            }
            BotLabels[0] = "오전";
            BotLabels[1] = "오후";
            BotLabels[2] = "심야";

            Formatter = value => value.ToString("N");
            DataContext = this;
        }
    }
}
