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
    public class Register : PageModel
    {
        [BindProperty] public user_controller User { get; set; } = new user_controller();
        public bool alertSuccess = false;
        public bool alertDanger = false;

        public void OnGet()
        {
            
        }
        
        public List<user_controller> Users
        {
            get
            {
                return new DBRepos().GetUsers();
            }
        }
        
        public void OnPostTryAddUser()
        {
            if (FormFilled())
            {
                if (CheckUsernameAvailable())
                {
                    new DBRepos().tryAddUser(User);
                    ClearUser();
                    alertSuccess = true;
                }
                else
                {
                    alertDanger = true;
                }
            }
        }

        private void ClearUser()
        {
            User.email = null;
            User.first_name = null;
            User.last_name = null;
            User.password = null;
            User.username = null;
        }

        private bool FormFilled()
        {
            if (User.email != null & User.first_name != null & User.password != null & User.last_name != null &
                User.username != null)
            {
                return true;
            }
            else return false;
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
    }
}