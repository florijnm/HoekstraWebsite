using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class TestPageOrdersToevoegenModel : PageModel
    {

        public void OnGet()
        {
        }

        public List<ValuePhoto> photo
        {
            get
            {
                return new DBRepos().GetPhotos();
            }
        }
    }
}
