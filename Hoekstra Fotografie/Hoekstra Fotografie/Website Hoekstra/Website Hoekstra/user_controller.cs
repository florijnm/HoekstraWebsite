using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Hoekstra
{
    public class user_controller
    {
        public string first_name{ get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public int user_id { get; set; }
        public bool admin { get; set; }
    }
}
