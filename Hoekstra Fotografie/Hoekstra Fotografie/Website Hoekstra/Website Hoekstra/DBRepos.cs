using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Website_Hoekstra
{
    public class DBRepos
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;Port=3306;Database=hoekstrafotografie;Uid=root;Pwd='';";
            return new MySqlConnection(connectionString);
        }
        public List<user_controller> Get()
        {
            var connect = Connect();
            List<user_controller> users = connect.Query<user_controller>(sql: "SELECT * FROM users").ToList();
            return users;
        }
    }
}
