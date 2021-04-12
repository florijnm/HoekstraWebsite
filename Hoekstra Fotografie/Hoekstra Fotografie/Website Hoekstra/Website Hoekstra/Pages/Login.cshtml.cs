using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace Website_Hoekstra.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public user_controller User { get; set; } = new user_controller();
        [BindProperty] public login_user LoginUser { get; set; } = new login_user();
        public bool alertSuccess = false;
        public bool alertDanger = false;

        public List<user_controller> Users
        {
            get
            {
                return new DBRepos().GetUsers();
            }
        }

        public void OnGet()
        {

        }


        public void OnPost()
        {

        }

        public void OnPostTryAddUser()
        {
            if (ModelState.IsValid)
            {
                if (CheckUsernameAvailable())
                {
                    new DBRepos().tryAddUser(User);
                }
            }
        }

        public void OnPostVerifyUser()
        {
            if (FormFilled())
            {
                if (VerifyPassword())
                {
                    alertSuccess = true;
                    DBRepos repos = new DBRepos();
                    user_controller user = repos.GetUserByUserID(LoginUser.loginUsername);
                    // login succesvol
                    HttpContext.Session.SetString("LoginSession", user.user_id.ToString());
                    if (user.admin == true)
                    {
                        Response.Redirect("adminpage");
                    }
                    else
                    {
                        Response.Redirect("index");
                    }
                }
                else
                {
                    alertDanger = true;
                }
            }
            else
            {
                alertDanger = true;
            }
        }
        
        private bool FormFilled()
        {
            if (LoginUser.loginPassword != null & LoginUser.loginUsername != null)
            {
                return true;
            }
            else return false;


        public bool CheckUsernameAvailable()
        {

            foreach (var user in Users)
            {
                if (User.username.Equals(user.username))
                {
                    return false;
                }
            }
            return true;
        }

        public bool VerifyPassword()
        {
            foreach (var dbUser in Users)
            {
                if (String.Compare(LoginUser.loginUsername, dbUser.username) == 0)
                {
                    if (new DBRepos().verifyPass(loginUser: LoginUser, dbUser.password))
                    {
                        User.admin = dbUser.admin;
                        User.username = dbUser.username;
                        User.password = dbUser.password;
                        User.username = dbUser.username;
                        User.user_id = dbUser.user_id;
                        User.password = dbUser.password;
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
