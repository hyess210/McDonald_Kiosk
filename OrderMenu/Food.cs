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
            public string ImgPath { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public int Amount { get; set; }
            public int Total { get; set; }
    }
}
