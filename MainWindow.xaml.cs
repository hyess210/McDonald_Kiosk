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
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }
    }
}
