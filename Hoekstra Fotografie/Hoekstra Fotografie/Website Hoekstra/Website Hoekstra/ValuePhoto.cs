using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Hoekstra
{
    public class ValuePhoto
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string path { get; set; }
        
        [Required]
        public float price { get; set; }
        
        public int picture_id { get; set; }
        
        [Required]
        public int category_id { get; set; }
        
    }

    
}
