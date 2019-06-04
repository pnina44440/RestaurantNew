using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class MenuSale
    {
        [Display(Name = "Menu")]
        [Key]
        [Column(Order = 1)]
        public string MenuId { get; set; }
        public Menu Menu { get; set; }

        [Display(Name = "Sale")]
        [Key]
        [Column(Order = 2)]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}