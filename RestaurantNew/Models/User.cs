﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantNew.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public ICollection<Order> Orders { get; set; }      
    }
}