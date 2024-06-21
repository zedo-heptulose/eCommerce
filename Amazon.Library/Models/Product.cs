using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eCommerce.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Id { get; set; }
        public int? Quantity { get; set; }

        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Price = 0;
            Id = 0;
        }
        public Product(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
        }
    }
}
