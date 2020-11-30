using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McDonald_Kiosk
{
    public class Customer
    {
        public int user_idx { get; set; }
        public string user_name { get; set; }
        public string user_barcode { get; set; }

        public string user_id { get; set; }
        public bool isAutoLogin { get; set; }

        public bool isGroupMessage { get; set; }

        public int order_idx { get; set; }
        public int tableNum { get; set; }
        public bool isCard { get; set; }

        private static Customer customer = new Customer();

        public static Customer getInstance()
        {
            if (customer == null)
                customer = new Customer();

            return customer;
        }
    }
}
