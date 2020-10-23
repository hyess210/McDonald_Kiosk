using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McDonald_Kiosk
{
    public class OrderState
    {
        public Category category { get; set; }
        public string Menu { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }

        private static List<OrderState> orderList;

        public static List<OrderState> GetInstance()
        {
            if (orderList == null) 
                orderList = new List<OrderState>();

            return orderList;
        }
    }
}