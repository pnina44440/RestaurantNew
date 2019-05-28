using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class MenuSale
    {
        [Display(Name = "Menu")]
        public string MenuId { get; set; }
        public Menu Menu { get; set; }

        [Display(Name = "Sale")]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}