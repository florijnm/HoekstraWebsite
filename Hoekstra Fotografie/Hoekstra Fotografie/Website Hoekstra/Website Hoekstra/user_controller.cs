using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Hoekstra
{
    public class user_controller
    {
<<<<<<< HEAD
        public string first_name{ get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public int user_id { get; set; }
=======
        [Required(AllowEmptyStrings = false)]
        public string first_name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string last_name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int user_id { get; set; }
        [Required(AllowEmptyStrings = false)]
>>>>>>> flori
        public bool admin { get; set; }
    }
}
