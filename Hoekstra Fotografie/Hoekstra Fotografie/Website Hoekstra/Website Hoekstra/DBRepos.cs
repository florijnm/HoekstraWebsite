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

        public bool AddPhoto(ValuePhoto photo)
        {
            var connect = Connect();
            int PhotoAdded = connect.Execute(
                @"INSERT INTO pictures(title, description, price, path, category_id) VALUES (@title, @description, @price, @path, @category_id)"
                , photo);

            return PhotoAdded == 1;
        }

        public List<category_ids> GetCategorie()
        {
            var connect = Connect();
            List<category_ids> categories = connect.Query<category_ids>(sql: "SELECT * FROM categories").ToList();
            return categories;
        }
    }
}
