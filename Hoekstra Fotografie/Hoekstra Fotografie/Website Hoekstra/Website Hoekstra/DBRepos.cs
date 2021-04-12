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

        // public List<user_controller> GetUsernames()
        // {
        //     var connect = Connect();
        //     List<user_controller> usernames = connect.Query<user_controller>("SELECT username FROM users").ToList();
        //     return usernames;
        // }

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
            return $"{hashedpass}:{Convert.ToBase64String(salt)}";;
        }

        public bool verifyPass(LoginUser userToCheck, string hashedPassWithSalt)
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

        public bool verifyPass(login_user loginUser, string hashedPassWithSalt)
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


            var hashOfPasswordToCheck = hashPass(loginUser.loginPassword, salt, true);
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
                                                                            price = @price, path = @path.FileName, category_id = @category_id
                                                                            WHERE picture_id = @picture_id;
                                                                            SELECT * FROM pictures WHERE picture_id = @picture_id", photo);
            return UpdatedPhoto;
        }

        public int PhotoOrder(PhotosOrdered photo)
        {
            var connect = Connect();
            int PhotoAdded = connect.Execute("INSERT INTO pictures_orders(order_id, picture_id) VALUES(@order_id ,@picture_id)", photo);
            return PhotoAdded;
        }

        public int NewOrder(user_controller user, PhotosOrdered PhotoId)
        {
            var connect = Connect();
            int NewOrder = connect.Execute("INSERT INTO orders(user_id) VALUES(@user_id)", user);
            return NewOrder;
        }

        public int PricePhoto(PhotosOrdered photo)
        {
            var connect = Connect();
            int price = connect.QuerySingleOrDefault<int>(sql: "SELECT price FROM pictures WHERE picture_id = @picture_id", photo);
            return price;
        }

        public int PriceTotal(PhotosOrdered photos)
        {
            var connect = Connect();
            int price = connect.QuerySingleOrDefault<int>("SELECT * FROM pictures p INNER JOIN pictures_orders po ON p.picture_id = po.picture_id WHERE po.order_id = @order_id", photos);
            return price;
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