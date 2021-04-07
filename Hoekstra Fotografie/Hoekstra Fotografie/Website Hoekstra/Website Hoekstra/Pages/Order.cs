using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Website_Hoekstra.Pages
{
    public class Order
    {
        public int order_id;
        public int user_id;
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;Port=3306;Database=hoekstrafotografie;Uid=root;Pwd=''";
            return new MySqlConnection(connectionString);
        }

        public List<Order> GetOrders()
        {
            var connect = Connect();
            List<Order> Orders = connect.Query<Order>(sql: "SELECT * FROM orders").ToList();
            return Orders;
        }
    }
}