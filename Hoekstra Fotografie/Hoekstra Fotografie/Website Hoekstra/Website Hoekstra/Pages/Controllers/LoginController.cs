using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Website_Hoekstra.Pages.Controllers
{
    public class LoginController : Controller
    {
        [BindProperty] public user_controller User { get; set; } = new user_controller();
        [BindProperty] public login_user LoginUser { get; set; } = new login_user();
        private string TestInput { get; set; }

        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // POST
        [HttpPost]
        public IActionResult TryLogin(string TestText)
        {
            Console.WriteLine("postdoet");
            Console.WriteLine(TestText);
            return Content("Header");
        }
    }
}