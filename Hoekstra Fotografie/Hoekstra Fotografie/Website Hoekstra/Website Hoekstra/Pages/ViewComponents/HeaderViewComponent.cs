using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Website_Hoekstra.Pages.ViewComponents
{
    public class testString
    {
        public string testStringName { get; set; }
    }
    
    public class LoginHandler
    {
        public string testStringName { get; set; }
        public user_controller UserList { get; set; }

        public string username;

    }
    
    public class HeaderViewComponent : ViewComponent
    {
        private user_controller User;
        private login_user LoginUser;
        private string Label;

        private string test;


        public HeaderViewComponent()
        {
            User = new user_controller();

            User.admin = false;
            User.email = "honossillie@gmail.com";
            User.first_name = "test";
            User.last_name = "Riemersma";
            User.password = "tC4J3YOw5Ev0oNHZfYzcZ3GvjF6h4sY8U7OIkGrx4Lc=:BMqmpMODnV+Kjhc38H2Apg==";
            User.username = "test";
            User.user_id = 22;
            
            LoginUser = new login_user();
            Label = "tyfuas";
            test = "tyfus";
        }

        public IViewComponentResult Invoke()
        {
            List<LoginHandler> LoginHandlers = new List<LoginHandler>();
            
            
            for (int i = 0; i < 10; i++)
            {
                LoginHandlers.Add(new LoginHandler()
                    {
                        
                        testStringName = "dude nummer : " + i,
                        UserList = User
                    }
                );
            }

            return View(LoginHandlers);
        }

        // public async Task<IViewComponentResult> InvokeAsync()
        // {
        //     return View("SignedOut");
        // }


        //
        // public async Task<IViewComponentResult> rInvokeAsync()
        // {
        //     var user = await User;
        //     return View(user);
        // }
        
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
            }
            else
            {
                Label = "false";
            }
        }
        public bool CheckUsernameAvailable()
        {

            foreach (var dbUser in Users)
            {
                if (User.username.Equals(dbUser.username))
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
                        if (new DBRepos().verifyPass(loginUser: LoginUser, dbUser.password))
                        {
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
            return false;
        }
    }
}