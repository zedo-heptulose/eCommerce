﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }
        public List<Product>? Contents { get; set; }
    }
}
