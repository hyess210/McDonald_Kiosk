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
        public SelectDiningTable()
        {
            InitializeComponent();

            List<Table> tables = new List<Table>();

            tableManage(tables);
            timerManage(tables);
        }

        private void tableManage(List<Table> tables)
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

        private void timerManage(List<Table> tables) 
        {
            for(int i = 0; i < tables.Count; i++)
            {
                Table table = tables[i];
                table.timer = new DispatcherTimer();
                table.timer.Interval = TimeSpan.FromSeconds(1);
                table.timer.Tick += (object s, EventArgs a) => timer_Tick(s, a, table);
                table.timer.Start();
            }
        }

        private void timer_Tick(object s, EventArgs a, Table table)
        {
            if(table.left_time > 0)
            {
                --table.left_time;
            } 
            else
            {
                table.timer.Stop();
            }
        }

        
    }
}
