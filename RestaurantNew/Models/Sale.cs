using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Discount { get; set; }

       // public ICollection<MenuSale> MenuSales { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}