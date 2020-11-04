using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace McDonald_Kiosk
{
    class Table
    {
        public int left_time = 0;
        public bool isEnabled = false;
        public DispatcherTimer timer;
    }
}
