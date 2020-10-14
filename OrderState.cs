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
        public string Menu { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }

        private static List<OrderState> orderState;

        public static List<OrderState> GetState()
        {
            if (orderState == null)
                orderState = new List<OrderState>();

            return orderState;
        }
    }
}