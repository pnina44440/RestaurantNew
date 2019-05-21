using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class Menu
    {
        [Key]
        public int IdMenu { get; set; }
        public string NameDose { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUri { get; set; }
        public int Categorya { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

    public class MenuViewModel
    {
        public string NameDose { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUri { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}