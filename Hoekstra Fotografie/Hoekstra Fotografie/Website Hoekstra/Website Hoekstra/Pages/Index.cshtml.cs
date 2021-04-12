using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Hoekstra.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        //pls kill
        private readonly user_controller User;
        private readonly login_user LoginUser;
        public string Label;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
            User = new user_controller();
            LoginUser = new login_user();
            Label = "test";
        }
        public List<user_controller> Users
        {
            get
            {
                return new DBRepos().GetUsers();
            }
        }
        
        public void OnGet()
        {
            DBRepos repos = new DBRepos();
            string SessionCookie = HttpContext.Session.GetString("LoginSession");
            if (SessionCookie != null)
            {
                user_controller user = repos.GetUserByID(Convert.ToInt32(SessionCookie));
                if (user.admin)
                {
                    Response.Redirect("adminpage");
                }
                else
                {
                    Response.Redirect("Error");
                }
            }
        }
    }
}
