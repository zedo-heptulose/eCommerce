using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Product> Contents { get; set; }

        public ShoppingCart()
        {
            Id = 0;
            Contents = new List<Product>();
        }
    }
}
