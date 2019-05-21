using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class City
    {
        [Key]
        public int IdCity { get; set; }
        public string NameCity { get; set; }

        
    }
}