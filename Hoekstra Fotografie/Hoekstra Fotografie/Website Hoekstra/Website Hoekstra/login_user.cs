using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Hoekstra
{
    public class login_user
    {
        [Required]
        public string loginEmail { get; set; }
        [Required]
        public string loginPassword { get; set; }
        [Required]
        public string loginUsername { get; set; }
    }
}
