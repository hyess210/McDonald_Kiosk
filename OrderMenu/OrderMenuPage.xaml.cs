using McDonald_Kiosk.OrderMenu;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderMenuPage : Page
    {

        private List<OrderMenu.Food> lstMenu = new List<OrderMenu.Food>();
        private List<OrderMenu.Food> menuList = new List<OrderMenu.Food>();
        private List<OrderMenu.Food> bugerList = new List<OrderMenu.Food>();
        private List<OrderMenu.Food> sideList = new List<OrderMenu.Food>();
        private List<OrderMenu.Food> drinkList = new List<OrderMenu.Food>();

        public int pageCount = 0;


        public OrderMenuPage()
        {
            this.Loaded += OrderMenuPage_Loaded;
            int i = 0;
            InitializeComponent();
            lbMenus.ItemsSource = lstMenu.Where(x => x.category == Category.BUGER).ToList();

            string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
                string sql = "SELECT * FROM menu";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    menuList.Add(new OrderMenu.Food()
                    {
                        Price = Int32.Parse(rdr["price"].ToString()),
                        Name = rdr["menu_name"].ToString(),
                        ImgPath = rdr["stored_path"].ToString(),
                        Menu_idx = Int32.Parse(rdr["menu_idx"].ToString())
                    });

                    string category = rdr["category"].ToString();

                    if (category.Equals("burger"))
                    {
                        menuList[i].category = Category.BUGER;
                    }
                    else if (category.Equals("side"))
                    {
                        menuList[i].category = Category.SIDE;
                    }
                    else if (category.Equals("drink"))
                    {
                        menuList[i].category = Category.DRINK;
                    }
                    i++;
                }
                rdr.Close();
                lbMenus.ItemsSource = lstMenu;
            }
        }

        private void OrderMenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetButtonEnable(OrderButton, false);
            SetButtonEnable(DeleteAllButton, false);

            for (int i = 0; i < menuList.Count; i++)
            {
                Debug.WriteLine(i);
                if (menuList[i].category.Equals(Category.BUGER))
                {
                    bugerList.Add(new OrderMenu.Food()
                    {
                        Price = menuList[i].Price,
                        Name = menuList[i].Name,
                        ImgPath = menuList[i].ImgPath,
                        Menu_idx = menuList[i].Menu_idx,
                        Amount = menuList[i].Amount
                    });
                }
                else if (menuList[i].category.Equals(Category.SIDE))
                {
                    sideList.Add(new OrderMenu.Food()
                    {
                        Price = menuList[i].Price,
                        Name = menuList[i].Name,
                        ImgPath = menuList[i].ImgPath,
                        Menu_idx = menuList[i].Menu_idx,
                        Amount = menuList[i].Amount
                    });
                }
                else if (menuList[i].category.Equals(Category.DRINK))
                {
                    drinkList.Add(new OrderMenu.Food()
                    {
                        Price = menuList[i].Price,
                        Name = menuList[i].Name,
                        ImgPath = menuList[i].ImgPath,
                        Menu_idx = menuList[i].Menu_idx,
                        Amount = menuList[i].Amount
                    });
                }
                else
                {
                    bugerList.Add(new OrderMenu.Food()
                    {
                        Price = menuList[i].Price,
                        Name = menuList[i].Name,
                        ImgPath = menuList[i].ImgPath,
                        Menu_idx = menuList[i].Menu_idx,
                        Amount = menuList[i].Amount
                    });
                }
            }
            MenuPageButton_Click(sender, e);
        }

        private void SetButtonEnable(Button button, bool isTrue)
        {
            if (isTrue)
            {
                button.IsEnabled = true;
            }
            else
            {
                button.IsEnabled = false;
            }
        }

        private void CheckLvAddedMenuEmpty()
        {
            if (OrderState.GetInstance().Count <= 0)
            {
                SetButtonEnable(OrderButton, false);
                SetButtonEnable(DeleteAllButton, false);
                return;
            }
            else
            {
                SetButtonEnable(OrderButton, true);
                SetButtonEnable(OrderButton, true);
                return;
            }
        }

        private void lbCategory_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1) return;
            Category category = (Category)lbCategory.SelectedIndex;
            lbMenus.ItemsSource = lstMenu.Where(x => x.category == category).ToList();

            pageCount = 0;
            MenuPageButton.IsEnabled = true;
            MenuBeforeButton.IsEnabled = true;
            MenuPageButton_Click(sender, e);
        }

        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isExist = false;
            Food food = lbMenus.SelectedItem as Food;

            SetButtonEnable(OrderButton, true);
            SetButtonEnable(DeleteAllButton, true);

            if (lbMenus.SelectedIndex == -1)
                return;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                if (OrderState.GetInstance()[i].Menu.Equals(food.Name))
                {
                    isExist = true;
                    OrderState.GetInstance()[i].Amount++;
                    OrderState.GetInstance()[i].Total = OrderState.GetInstance()[i].Amount * OrderState.GetInstance()[i].Price;
                }

            }
            if (!isExist)
            {
                OrderState.GetInstance().Add(new OrderState()
                {
                    category = food.category,
                    Menu = food.Name,
                    Price = food.Price,
                    Amount = 1,
                    Total = food.Price,
                    Menu_idx = food.Menu_idx
                });
            }

            lbMenus.UnselectAll();
            lvAddedMenu.ItemsSource = OrderState.GetInstance();
            lvAddedMenu.Items.Refresh();
        }

        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            void DeleteAllMenu()
            {
                OrderState.GetInstance().Clear();
                lvAddedMenu.Items.Refresh();

                OrderButton.IsEnabled = false;
                DeleteAllButton.IsEnabled = false;
            }

            if (OrderState.GetInstance().Count > 0)
            {
                MessageBoxResult m = MessageBox.Show("선택하신 모든 메뉴가 삭제됩니다.", "모두 삭제 하시겠습니까?", MessageBoxButton.YesNo);
                if (m == MessageBoxResult.Yes)
                {
                    DeleteAllMenu();
                    return;
                }
                else if (m == MessageBoxResult.No)
                {
                    return;
                }
            }
            else
            {
                DeleteAllMenu();
            }
        }

        private void MenuAddButton_Click(object sender, RoutedEventArgs e)
        {
            OrderState BtnMenu = (sender as Button).DataContext as OrderState;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                if (OrderState.GetInstance()[i].Menu.Equals(BtnMenu.Menu))
                {
                    OrderState.GetInstance()[i].Amount++;
                    OrderState.GetInstance()[i].Total = OrderState.GetInstance()[i].Amount * OrderState.GetInstance()[i].Price;
                }
            }
            lvAddedMenu.Items.Refresh();
        }

        private void MenuMinusButton_Click(object sender, RoutedEventArgs e)
        {
            OrderState BtnMenu = (sender as Button).DataContext as OrderState;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                if (OrderState.GetInstance()[i].Menu.Equals(BtnMenu.Menu) && OrderState.GetInstance()[i].Amount > 1)
                {
                    OrderState.GetInstance()[i].Amount--;
                    OrderState.GetInstance()[i].Total = OrderState.GetInstance()[i].Amount * OrderState.GetInstance()[i].Price;
                }
                else if (OrderState.GetInstance()[i].Menu.Equals(BtnMenu.Menu))
                {
                    OrderState.GetInstance().Remove(OrderState.GetInstance()[i]);
                    CheckLvAddedMenuEmpty();
                }
            }
            lvAddedMenu.Items.Refresh();
        }
        private void MenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            OrderState BtnMenu = (sender as Button).DataContext as OrderState;

            for (int i = 0; i < OrderState.GetInstance().Count; i++)
            {
                if (OrderState.GetInstance()[i].Menu.Equals(BtnMenu.Menu))
                {
                    OrderState.GetInstance().Remove(OrderState.GetInstance()[i]);
                    CheckLvAddedMenuEmpty();
                }
            }
            lvAddedMenu.Items.Refresh();
        }

        private void GoDiningSelect_ButtonClick(object sender, RoutedEventArgs e)
        {
            SelectDiningPlace select = new SelectDiningPlace();
            NavigationService.Navigate(select);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectPayment selectPayment = new SelectPayment();
            if (NavigationService.CanGoBack)
            {
                if (OrderState.GetInstance().Count > 0)
                {
                    MessageBoxResult m = MessageBox.Show("선택하신 모든 메뉴가 삭제됩니다.", "이전 페이지로 가시겠습니까?", MessageBoxButton.YesNo);
                    if (m == MessageBoxResult.Yes)
                    {
                        OrderState.GetInstance().Clear();
                        NavigationService.GoBack();
                        return;
                    }
                    else if (m == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void MenuMovePage(List<Food> foods)
        {
            for (int i = 0; i < 9; i++)
            {
                pageCount++;
                if (foods.Count <= pageCount)
                {
                    MenuPageButton.IsEnabled = false;
                    break;
                }
                else if (pageCount <= 9)
                {
                    MenuPageButton.IsEnabled = true;
                    MenuBeforeButton.IsEnabled = false;
                    lstMenu.Add(foods[pageCount]);
                }
                else
                {
                    MenuPageButton.IsEnabled = true;
                    MenuBeforeButton.IsEnabled = true;
                    lstMenu.Add(foods[pageCount]);
                }
            }

        }

        private void MenuPageButton_Click(object sender, RoutedEventArgs e)
        {
            lstMenu.Clear();
            Category category = (Category)lbCategory.SelectedIndex;
            if (category.Equals(Category.BUGER))
            {
                MenuMovePage(bugerList);
            }
            else if (category.Equals(Category.DRINK))
            {
                MenuMovePage(drinkList);
            }
            else if (category.Equals(Category.SIDE))
            {
                MenuMovePage(sideList);
            }
            else
            {
                MenuMovePage(bugerList);
            }
            lbMenus.Items.Refresh();
            lbMenus.ItemsSource = lstMenu;
        }

        private void MenuBeforeButton_Click(object sender, RoutedEventArgs e)
        {
            pageCount -= pageCount;
            MenuPageButton_Click(sender, e);
        }

        //private void DiningPlace_Closed (object sender, RoutedEventArgs e)
        //{
        //    SelectPayment selectPayment = new SelectPayment();
        //    NavigationService.Navigate(selectPayment);
        //}
    }
}
