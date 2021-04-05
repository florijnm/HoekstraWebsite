using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
       {
        [BindProperty] public user_controller User { get; set; } = new user_controller();
        [BindProperty] public login_user LoginUser { get; set; } = new login_user();
        [BindProperty] public string Label { get; set; }

        public string test = "";


        public HeaderViewComponent(string tester, string jonge = "dipshit")
        {
            test = tester;
        }
        
        public string InvokeAsync()
        {
            test += test;
            return test;
        }
        
        
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
                        if (new DBRepos().verifyPass(loginUser: LoginUser, password: dbUser.password))
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