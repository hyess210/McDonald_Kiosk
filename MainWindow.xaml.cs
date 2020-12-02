using McDonald_Kiosk.OrderMenu;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            DateLabel.Content = DateTime.Now.ToString("f");
            Properties.Settings.Default.isAutoLogin = false;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderState.GetInstance().Count > 0)
            {
                MessageBoxResult m = MessageBox.Show("선택하신 모든 메뉴가 삭제됩니다.", "이전 페이지로 가시겠습니까?", MessageBoxButton.YesNo);
                if (m == MessageBoxResult.Yes)
                {
                    OrderMenuPage order = new OrderMenuPage();
                    OrderState.GetInstance().Clear();
                    //MainContent.Navigate(new Uri("Home.xaml", UriKind.Relative));
                    MainContent.Navigate(new Home());
                }
            }
            else
            {
                OrderState.GetInstance().Clear();
                //MainContent.Navigate(new Uri("Home.xaml", UriKind.Relative));
                MainContent.Navigate(new Home());

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Message message = new Message();
            message.ShowDialog();
        }
    }
}
