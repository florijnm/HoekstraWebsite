using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
