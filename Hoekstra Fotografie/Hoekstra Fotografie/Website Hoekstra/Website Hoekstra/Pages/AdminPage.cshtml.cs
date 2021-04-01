using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Website_Hoekstra.Pages
{
    public class AdminPageModel : PageModel
    {
        [BindProperty] public ValuePhoto NewPhoto { get; set; } = new ValuePhoto();
        [BindProperty] public string Label { get; set; }
        public void OnGet()
        {
            //string cookieStr = Request.Cookies["cookieLogin"];
            string SessionCookie = HttpContext.Session.GetString("LoginSession");
            DBRepos repos = new DBRepos();
            user_controller user = repos.GetUserByID(Convert.ToInt32(SessionCookie));
            if (SessionCookie != null)
            {
                Label = "Welcome, " + FirstLetterToUpper(user.username.ToString()) + "!";
            }
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
            {
                return null;
            }
            if (str.Length > 1) 
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return str.ToUpper();
        }

        public List<category_ids> category
        {
            get
            {
                return new DBRepos().GetCategorie();
            }
        }

        public void OnPost()
        {

        }

        public void OnPostAddPhoto()
        {
            if (ModelState.IsValid)
            {
                new DBRepos().AddPhoto(NewPhoto);
                ModelState.Clear();
            }
            
        }
    }
}
