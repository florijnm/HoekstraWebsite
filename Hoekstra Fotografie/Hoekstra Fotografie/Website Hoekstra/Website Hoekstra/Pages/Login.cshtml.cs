using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public user_controller User { get; set; } = new user_controller();

        [BindProperty] public login_user LoginUser { get; set; } = new login_user();
        [BindProperty] public string Label { get; set; }

        public List<user_controller> Users
        {
            get
            {
                return new DBRepos().GetUsers();
            }
        }

        public void OnGet()
        {
            Label = "false";
            
        }
        

        public void OnPost()
        {
            
        }

        public void OnPostTryAddUser()
        {
            if (CheckUsernameAvailable())
            {
                new DBRepos().tryAddUser(User);
            }
            else
            {
                
            }
        }

        public void OnPostVerifyUser()
        {
            if (VerifyPassword())
            {
                // login succesvol
                Label = "true";
<<<<<<< HEAD
=======
                Response.Cookies.Append("cookieLogin", LoginUser.loginUsername);
                if (User.admin == true)
                {
                    Response.Redirect("adminpage");
                }
                else
                {
                    Response.Redirect("Error");
                }
>>>>>>> flori
            }
            else
            {
                Label = "false";
            }
        }
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
            if (LoginUser.loginUsername != null && LoginUser.loginUsername != null )
            {
                foreach (var dbUser in Users)
                {
                    if (String.Compare(LoginUser.loginUsername,dbUser.username) == 0)
                    {
                        if (new DBRepos().verifyPass(LoginUser, dbUser.password))
                        {
<<<<<<< HEAD
=======
                            User.admin = dbUser.admin;
                            User.username = dbUser.username;
                            User.password = dbUser.password;
>>>>>>> flori
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }
            return false;
        }
    }
}