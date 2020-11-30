using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface DB
{
    void InsertData(int order_idx, int user_idx, int tableNum, bool isCard);
    void InsertData(int menu_idx, int order_idx, int amount, int total);
}