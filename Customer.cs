using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McDonald_Kiosk
{
    public class Customer
    {
        public int order_idx;
        public int user_idx;
        public DateTime order_time;
        public int tableNum;

        private static Customer customer = new Customer();

        public Customer getInstance()
        {
            if (customer == null)
                customer = new Customer();

            return customer;
        }
    }
}
