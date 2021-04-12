using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Website_Hoekstra.Pages
{
    public class login_user
    {
        public string loginEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vul een wachtwoord in"), MinLength(2)]
        public string loginPassword { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage = "Vul een username in"), MinLength(2)]
        public string loginUsername { get; set; }
    }
}