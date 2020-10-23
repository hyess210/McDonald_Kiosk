using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Label> timeText = new List<Label>();
        public SelectDiningTable()
        {
            InitializeComponent();

            

            tableManage();
            timeTextManage();
            timerManage();
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
            timeText.Add(timer1);
            timeText.Add(timer2);
            timeText.Add(timer3);
            timeText.Add(timer4);
            timeText.Add(timer5);
            timeText.Add(timer6);
            timeText.Add(timer7);
            timeText.Add(timer8);
            timeText.Add(timer9);
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
            for(int i = 0; i < 9; i++)
            {
                if (!tables[i].isEnabled)
                {
                    if (tables[i].left_time < 1)
                        tables[i].isEnabled = true;
                    else
                        --tables[i].left_time;
                }
                leftTimeMapping(i);
            }
        }

        private void leftTimeMapping(int idx)
        {
            if(tables[idx].isEnabled)
                timeText[idx].Content = "";
            else
                timeText[idx].Content = tables[idx].left_time;
        }
    }
}
