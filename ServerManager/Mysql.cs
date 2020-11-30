using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

class Mysql : DB
{
    static string connStr = "Server=10.80.162.193;Database=mcdonald_kiosk;Uid=root;Pwd=kmk5632980;";
    MySqlConnection connection = new MySqlConnection(connStr);

    public void InsertData(int order_idx, int user_idx, int tableNum, bool isCard)
    {
        string sql = "Insert INTO ordering(order_idx, user_idx, tableNum, isCard) " +
            "VALUES (" + order_idx + ',' + user_idx + ',' + tableNum + ',' + isCard + ")";

        connection.Open();
        MySqlCommand command = new MySqlCommand(sql, connection);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        connection.Close();
    }

    public void InsertData(int menu_idx, int order_idx, int amount, int total)
    {
        string sql = "Insert INTO ordered_menu(menu_idx, order_idx, amount, total) " +
            "VALUES (" + menu_idx + ',' + order_idx + ',' + amount + ',' + total +")";

        connection.Open();
        MySqlCommand command = new MySqlCommand(sql, connection);

        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        connection.Close();
    }
}