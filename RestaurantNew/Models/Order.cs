using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        public int IdClub { get; set; }
        public int IdMenu { get; set; }
        public DateTime DateForDay { get; set; }
        public int Count { get; set; }

        public virtual Menu Menus { get; set; }
    }
}