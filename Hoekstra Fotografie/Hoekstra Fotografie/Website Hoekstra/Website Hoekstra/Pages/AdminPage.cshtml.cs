using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class AdminPageModel : PageModel
    {
        [BindProperty] public ValuePhoto NewPhoto { get; set; } = new ValuePhoto();

        

        public void OnGet()
        {

        }

        public List<category_ids> category
        {
            get
            {
                return new DBRepos().GetCategorie();
            }
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
                new DBRepos().AddPhoto(NewPhoto);
                //IFormFile file = Request.Form.Files["path"];
                //using (FileStream fs = new FileStream("wwwroot/Images", FileMode.CreateNew, FileAccess.Write, FileShare.Write))
                //{
                //    file.CopyTo(fs);
                //}

            }
        }

        public void OnPostManageOrders(int picture_id)
        {
            new DBRepos().DeletePhoto(picture_id);
        }
    }
}
