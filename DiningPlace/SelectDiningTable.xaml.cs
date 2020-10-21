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

            Table table1 = new Table();
            createObject(table1);
            Table table2 = new Table();
            createObject(table2);
            Table table3 = new Table();
            createObject(table3);
            Table table4 = new Table();
            createObject(table4);
            Table table5 = new Table();
            createObject(table5);
            Table table6 = new Table();
            createObject(table6);
            Table table7 = new Table();
            createObject(table7);
            Table table8 = new Table();
            createObject(table8);
            Table table9 = new Table();
            createObject(table9);
        }

        private void createObject(Table table)
        {
            table.timer = new DispatcherTimer();
            table.timer.Interval = TimeSpan.FromSeconds(1);
            table.timer.Tick += (object s, EventArgs a) => timer_Tick(s, a, table);
            table.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e, Table table)
        {
            DateTime dateTime = new DateTime(2020, 10, 20, 11, 58, 0); // DB로부터 시간 받아옴.
            TimeSpan timeSpan = DateTime.Now - dateTime;
            if (timeSpan.CompareTo(new TimeSpan(0, 0, 59)) > 0)
            {
                table.timer.Stop();
            }
        }


    }
}
