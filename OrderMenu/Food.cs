using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McDonald_Kiosk.OrderMenu
{
    public class Food
    {
            public Category category { get; set; }
            public string imgPath { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public int amount { get; set; }
    }
}
