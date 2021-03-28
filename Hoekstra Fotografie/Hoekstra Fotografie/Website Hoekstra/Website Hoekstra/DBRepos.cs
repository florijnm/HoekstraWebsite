using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Website_Hoekstra
{
    public class DBRepos
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;Port=3306;Database=hoekstrafotografie;Uid=root;Pwd='';AllowUserVariables=True;";
            return new MySqlConnection(connectionString);
        }
        public List<user_controller> GetUsers()
        {
            var connect = Connect();
            List<user_controller> users = connect.Query<user_controller>(sql: "SELECT * FROM users").ToList();
            return users;
        }

        // public List<string> GetUsernames()
        // {
        //     var connect = Connect();
        //     List<string> usernamess = connect.Query("SELECT username FROM users")
        //
        //     List<user_controller> usernamess = GetUsers();
        //
        //     
        //     List<string> usernames = usernamess.
        //     return usernames;
        // }

        public bool AddPhoto(ValuePhoto photo)
        {
            Console.WriteLine("test");
            Console.WriteLine(photo);
            var connect = Connect();
            int PhotoAdded = connect.Execute(
                @"INSERT INTO pictures(title, description, price, path, category_id) VALUES (@title, @description, @price, @path, @category_id)"
                , photo);

            return PhotoAdded == 1;
        }

        public bool tryAddUser(user_controller newUser)
        {
            Console.WriteLine("test");
            Console.WriteLine(newUser);
            string hashedPass = hashPass(newUser);
            newUser.password = hashedPass;
            Console.WriteLine("hashedpass: " + hashedPass);
            var connect = Connect();
            connect.Execute(@"INSERT INTO users (email, first_name, last_name, password, username, admin, user_id) VALUES (@email, @first_name, @last_name, @password, @username, @admin, @user_id)"
            , newUser);
            return true;
        }

        public string hashPass(user_controller newUser)
        {
            byte[] salt = new byte[128 / 8];
            string password = newUser.password;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedpass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedpass;
        }
        
        public List<category_ids> GetCategorie()
        {
            var connect = Connect();
            List<category_ids> categories = connect.Query<category_ids>(sql: "SELECT * FROM categories").ToList();
            return categories;
        }

        public List<ValuePhoto> GetPhotos()
        {
            var connect = Connect();
            List<ValuePhoto> photos = connect.Query<ValuePhoto>(sql: "SELECT * FROM pictures").ToList();
            return photos;
        }

        public ValuePhoto GetById(int Photo_id)
        {
            var connect = Connect();
            var photo = connect.QuerySingleOrDefault<ValuePhoto>(sql: "SELECT * FROM pictures WHERE picture_id = @picture_id",
                new { picture_id = Photo_id });
            return photo;
        }

        public bool DeletePhoto(int Picture_Id)
        {
            var connect = Connect();
            int DeletedPhotos = connect.Execute(sql: "DELETE FROM pictures WHERE picture_id = @picture_id",
                new { picture_id = Picture_Id });
            return DeletedPhotos == 1;
        }

        public ValuePhoto Update(ValuePhoto photo)
        {
            var connect = Connect();
            ValuePhoto UpdatedPhoto = connect.QuerySingle<ValuePhoto>(sql: @"UPDATE pictures SET title = @title, description = @description, 
                                                                            price = @price, path = @path, category_id = @category_id
                                                                            WHERE picture_id = @picture_id;
                                                                            SELECT * FROM pictures WHERE picture_id = @picture_id", photo);
            return UpdatedPhoto;
        }
    }
}
