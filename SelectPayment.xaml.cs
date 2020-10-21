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
    /// SelectPayment.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SelectPayment : Page
    {
        public SelectPayment()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CashPayment cash = new CashPayment();
            NavigationService.Navigate(cash);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CardPayment card = new CardPayment();
            NavigationService.Navigate(card);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void ListView_Load(object sender, RoutedEventArgs e)
        {
            ListView listview = sender as ListView;
            //OrderState.GetInstance().Add(new OrderState() { Menu = "불고기 버거", Price = 3000, Amount = 2, Total = 6000 });
            //OrderState.GetInstance().Add(new OrderState() { Menu = "새우 버거", Price = 3500, Amount = 1, Total = 3500 });
            listview.ItemsSource = OrderState.GetInstance();
        }
    }
}