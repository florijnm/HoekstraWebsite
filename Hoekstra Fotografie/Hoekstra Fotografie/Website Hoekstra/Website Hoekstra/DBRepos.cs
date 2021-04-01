using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Website_Hoekstra.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Website_Hoekstra
{
    public class DBRepos
    {
        public IDbConnection Connect()
        {
            string connectionString = @"Server=127.0.0.1;Port=3306;Database=hoekstrafotografie;Uid=root;Pwd=''";
            return new MySqlConnection(connectionString);
        }
        public List<user_controller> GetUsers()
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

        public bool tryAddUser(user_controller newUser)
        {
            string hashedPass = hashPass(newUser.password);
            newUser.password = hashedPass;
            var connect = Connect();
            connect.Execute(@"INSERT INTO users (email, first_name, last_name, password, username, admin, user_id) VALUES (@email, @first_name, @last_name, @password, @username, @admin, @user_id)"
            , newUser);
            return true;
        }

        public string hashPass(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            string hashedpass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash) return hashedpass;
            return $"{hashedpass}:{Convert.ToBase64String(salt)}"; ;
        }

        public bool verifyPass(login_user userToCheck, string hashedPassWithSalt)
        {
            var passwordAndHash = hashedPassWithSalt.Split(':');
            if (passwordAndHash == null || passwordAndHash.Length != 2)
            {
                return false;

            }
            var salt = Convert.FromBase64String(passwordAndHash[1]);
            if (salt == null)
            {
                return false;

            }

            var hashOfPasswordToCheck = hashPass(userToCheck.loginPassword, salt, true);
            if (String.Compare(passwordAndHash[0], hashOfPasswordToCheck) == 0)
            {
                return true;
            }
            return false;

        }

        public List<category_ids> GetCategorie()
        {
            var connect = Connect();
            List<category_ids> categories = connect.Query<category_ids>(sql: "SELECT * FROM categories").ToList();
            return categories;
        }
       public user_controller UserByID = new user_controller();


        public user_controller GetUserByUserID(string username)
        {
            var connect = Connect();
            List<user_controller> usercheckers = connect.Query<user_controller>(sql: "SELECT * FROM users").ToList();
            foreach (var user in usercheckers)
            {
                if (user.username == username)
                {
                    UserByID = user;
                    return UserByID;
                }
            }
            return null;
        }

        public user_controller UserByTheirID = new user_controller();
        public user_controller GetUserByID(int user_id)
        {
            var connect = Connect();
            List<user_controller> getuser = connect.Query<user_controller>(sql: "SELECT * FROM users").ToList();
            foreach (var user in getuser)
            {
                if (user.user_id == user_id)
                {
                    UserByTheirID = user;
                    return UserByTheirID;
                }
            }
            return null;
        }
    }
}