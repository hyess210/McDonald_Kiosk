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

namespace McDonald_Kiosk
{
    /// <summary>
    /// SelectDiningPlace.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectDiningPlace : Page
    {
        public SelectDiningPlace()
        {
            InitializeComponent();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectDiningTable selectTable = new SelectDiningTable();
            NavigationService.Navigate(selectTable);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            goToPay.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
