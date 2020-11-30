using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace McDonald_Kiosk
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            checkOverlap(id_TextBox.Text, pw_TextBox.Text);
            SendMessage();
        }

        private void checkOverlap(string id, string pw)
        {
            if (id == "baejoohyun" && pw == "a1234567")
            {
                Customer.getInstance().user_id = id_TextBox.Text;
                //SendMessage();
                Window.GetWindow(this).Close();
            }
            else
                MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.isAutoLogin = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.isAutoLogin = false;
        }

        private void SendMessage()
        {
            try {
                TcpClient tcp = new TcpClient("10.80.163.155", 80);
                var json = new JObject();
                json.Add("MSGType", 0);
                json.Add("Id", "2211");
                json.Add("Group", false);
                byte[] buff = Encoding.UTF8.GetBytes(json.ToString());
                NetworkStream network = tcp.GetStream();
                network.Write(buff, 0, buff.Length);
            } 
            catch(SocketException e)
            {
                MessageBox.Show("서버와 연결이 되지 않습니다.");
            }
        }
    }
}
