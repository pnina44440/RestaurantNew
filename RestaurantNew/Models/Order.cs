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
        public int Id { get; set; }
        public CustUser User { get; set; }
        public Menu Menu { get; set; }
        public DateTime DateForDay { get; set; }
        public int Count { get; set; }
        
    }
}