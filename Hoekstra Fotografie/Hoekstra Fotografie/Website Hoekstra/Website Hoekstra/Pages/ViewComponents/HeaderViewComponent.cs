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

        public void OnPostTryLogin()
        {
            Console.WriteLine("in trylogin in component");
        }
    }
}