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
        [BindProperty] public ValuePhoto NewPhoto { get; set; } = new ValuePhoto();
        [BindProperty] public user_controller NewUser { get; set; } = new user_controller();
        
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
            if (checkUsernameAvailable())
            {
                new DBRepos().tryAddUser(NewUser);
            }
            else
            {
                
            }
        }

        public bool checkUsernameAvailable()
        {

            foreach (var user in Users)
            {
                if (NewUser.username.Equals(user.username))
                {
                    Console.WriteLine("gebruiker bestaat");
                    return false;
                }
            }
            Console.WriteLine("gebruiker bestaat NIET");
            return true;
        }
    }
}