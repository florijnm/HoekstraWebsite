using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class UpdatePhotoModel : PageModel
    {
        [BindProperty] public ValuePhoto UpdatePhoto { get; set; }

        public void OnGet(int picture_id)
        {
            UpdatePhoto = new DBRepos().GetById(picture_id);
        }

        public void OnPost(int picture_id)
        {
            UpdatePhoto = new DBRepos().GetById(picture_id);
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                new DBRepos().Update(UpdatePhoto);
                return RedirectToPage("AdminPage");
            }

            return Page();
        }
    }
}
