using McDonald_Kiosk.OrderMenu;
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
        public OrderMenuPage()
        {
            InitializeComponent();
            lbMenus.ItemsSource = lstMenu.Where(x => x.category == Category.BUGER).ToList();
            //lvAddedMenu.
        }

        //public class MenuList
        //{
        //    public Category category { get; set; }
        //    public string ImgPath { get; set; }
        //    public string Name { get; set; }
        //}


        private void lbCategory_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lbCategory.SelectedIndex == -1) return;
            Category category = (Category)lbCategory.SelectedIndex;
            lbMenus.ItemsSource = lstMenu.Where(x => x.category == category).ToList();
        }
            
        private List<OrderMenu.Food> lstMenu = new List<OrderMenu.Food>()
        {
            new OrderMenu.Food() { category = Category.BUGER, Name="빅 맥", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727841393.png", Price = 4900},
            new OrderMenu.Food() { category = Category.BUGER, Name="케이준 맥치킨", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1600413692852.png", Price = 5100 },
            new OrderMenu.Food() { category = Category.BUGER, Name="맥스파이시 상하이 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583728339451.png", Price = 4600 },
            new OrderMenu.Food() { category = Category.BUGER, Name="1995 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1599119588089.png", Price = 5500 },
            new OrderMenu.Food() { category = Category.BUGER, Name="맥치킨 모짜렐라", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727633823.png", Price = 4700 },
            new OrderMenu.Food() { category = Category.BUGER, Name="더블 1995 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727223345.png", Price = 7000 },
            new OrderMenu.Food() { category = Category.BUGER, Name="더블 불고기 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727299888.png", Price = 4500 },
            new OrderMenu.Food() { category = Category.BUGER, Name="에그 불고기 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1599100021723.png", Price = 4500 },
            new OrderMenu.Food() { category = Category.BUGER, Name="슈비 버거", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727896884.png" , Price = 5500},
            new OrderMenu.Food() { category = Category.BUGER, Name="베이컨 토마토 디럭스", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727690227.png", Price = 5100 },
            new OrderMenu.Food() { category = Category.BUGER, Name="더블 쿼터파운더 치즈", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583727487454.png", Price = 6700 },
            new OrderMenu.Food() { category = Category.BUGER, Name="에그 맥머핀", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290308474630.png", Price = 2500 },
            new OrderMenu.Food() { category = Category.BUGER, Name="베이컨 토마토 머핀", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290308359530.png", Price = 3200 },
            new OrderMenu.Food() { category = Category.BUGER, Name="소세지 에그 맥머핀", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201903190244458360.png", Price = 3000 },

            new OrderMenu.Food() { category = Category.SIDE, Name="타로 파이", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1600755612822.png", Price = 1000 },
            new OrderMenu.Food() { category = Category.SIDE, Name="웨지 후라이", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1599099116070.png", Price = 2100 },
            new OrderMenu.Food() { category = Category.SIDE, Name="맥스파이시 상하이 치킨 스낵랩", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201902080435011620.png", Price = 2600 },
            new OrderMenu.Food() { category = Category.SIDE, Name="애플 파이", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201902040342215990.png", Price = 1000 },
            new OrderMenu.Food() { category = Category.SIDE, Name="골든 모짜렐라 치즈스틱", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201902080435496530.png", Price = 2600 },
            new OrderMenu.Food() { category = Category.SIDE, Name="후렌치 후라이", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201902070318045120.png", Price = 1700 },
            new OrderMenu.Food() { category = Category.SIDE, Name="맥스파이시 치킨 텐더", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201903120346584950.png", Price = 8800 },
            new OrderMenu.Food() { category = Category.SIDE, Name="해쉬 브라운", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1563759767701.jpg", Price = 1000 },
            new OrderMenu.Food() { category = Category.SIDE, Name="바나나 오레오 맥플러리", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1590473591953.png", Price = 2500 },
            new OrderMenu.Food() { category = Category.SIDE, Name="오레오 맥플러리", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201905140116054120.png", Price = 2500 },
            new OrderMenu.Food() { category = Category.SIDE, Name="딸기 오레오 맥플러리", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201905140116378240.png", Price = 2500 },
            new OrderMenu.Food() { category = Category.SIDE, Name="초코 선데이 아이스크림", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290248295110.png", Price = 1500 }
            ,
            new OrderMenu.Food() { category = Category.DRINK, Name="배 칠러", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1584411193442.png", Price = 2500},
            new OrderMenu.Food() { category = Category.DRINK, Name="자두 칠러", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290217064420.png", Price = 2500},
            new OrderMenu.Food() { category = Category.DRINK, Name="카페 라떼", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290234398270.png", Price = 2500},
            new OrderMenu.Food() { category = Category.DRINK, Name="디카페인 카페 라떼", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1577432576871.jpg", Price = 2500},
            new OrderMenu.Food() { category = Category.DRINK, Name="카푸치노", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290235239890.png", Price = 2500},
            new OrderMenu.Food() { category = Category.DRINK, Name="아메리카노", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290232543290.png", Price = 2000},
            new OrderMenu.Food() { category = Category.DRINK, Name="에스프레소", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201902121106138080.png", Price = 2100},
            new OrderMenu.Food() { category = Category.DRINK, Name="프리미엄 로스트 원두 커피", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290228358090.png", Price = 2100},
            new OrderMenu.Food() { category = Category.DRINK, Name="바닐라 쉐이크", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290255488970.png", Price = 3100},
            new OrderMenu.Food() { category = Category.DRINK, Name="딸기 쉐이크", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290257347040.png", Price = 3100},
            new OrderMenu.Food() { category = Category.DRINK, Name="초코 쉐이크", ImgPath="https://www.mcdonalds.co.kr/uploadFolder/product/prol_201901290257444640.png", Price = 3100},
            new OrderMenu.Food() { category = Category.DRINK, Name="코카 콜라", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583889953745.png", Price = 1600},
            new OrderMenu.Food() { category = Category.DRINK, Name="스프라이트", ImgPath="https://www.mcdonalds.co.kr/upload/product/pcList/1583889827271.png", Price = 1600}
        };

        private void lbMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isExist = false;
            Food food = lbMenus.SelectedItem as Food;

            if (lbMenus.SelectedIndex == -1) 
                return;
            
            for(int i=0; i < OrderState.GetInstance().Count; i++)
            {
                if(OrderState.GetInstance()[i].Menu.Equals(food.Name))
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
                    Amount = food.Amount,
                    Total = 1
                });
            }
            lvAddedMenu.ItemsSource = OrderState.GetInstance();
            lvAddedMenu.Items.Refresh();
        }

        private void DeleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            OrderState.GetInstance().Clear();
            lvAddedMenu.Items.Refresh();
        }

        private void MenuAddButton_Click(object sender, RoutedEventArgs e)
        {
            OrderState BtnMenu = (sender as Button).DataContext as OrderState;
            
            for ( int i = 0; i < OrderState.GetInstance().Count; i++)
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
                } else if (OrderState.GetInstance()[i].Menu.Equals(BtnMenu.Menu)) { 
                    OrderState.GetInstance().Remove(OrderState.GetInstance()[i]);
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
                }
            }
            lvAddedMenu.Items.Refresh();
        }

        private void GoPayment_ButtonClick(object sender, RoutedEventArgs e)
        {
            SelectPayment selectPayment = new SelectPayment();
            NavigationService.Navigate(selectPayment);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectPayment selectPayment = new SelectPayment();
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

    }
}
