using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class AdminPageModel : PageModel
    {
        [BindProperty] public IFormFile PhotoImport { get; set; }
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

        public List<Order> Orders
        {
            get
            {
                return new Order().GetOrders();
            }
        }

        public string UserByOrder(int user_id)
        {
            user_controller UserById = new DBRepos().GetUserByID(user_id);
            return UserById.username;
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

        public int TotalPhotoByOrder(int Order_id)
        {
            int TotalPhotos = new DBRepos().GetPhotosOrder(Order_id);
            return TotalPhotos;
        }

        public List<ValuePhoto> photo
        {
            get
            {
                return new DBRepos().GetPhotos();
            }
        }

        public void OnPost()
        {
            
        }

        
        public void OnPostAddPhoto()
        {
            if (ModelState.IsValid)
            {
                PhotoImport = Request.Form.Files["path"];
                new DBRepos().AddPhoto(NewPhoto);
                using (FileStream fs = new FileStream("wwwroot/Images", FileMode.CreateNew, FileAccess.Write, FileShare.Write))
                {
                    PhotoImport.CopyTo(fs);
                }
            }
        }

        public void OnPostManageOrders(int picture_id)
        {
            new DBRepos().DeletePhoto(picture_id);
        }
    }
}
