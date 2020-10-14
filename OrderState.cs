using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McDonald_Kiosk
{
    class OrderState
    {
        public string Menu;
        public int Price;
        public int Amount;
        public int Total;

        public static List<OrderState> orderState;
    }
}