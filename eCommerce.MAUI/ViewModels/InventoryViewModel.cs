using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using eCommerce.Library.Services;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel
    {
        public List<Product> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.ToList() ?? new List<Product>();
            }
        }

        public Product SelectedProduct { get; set; }

        public void UpdateContact()
        {
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct);
        }

        public InventoryViewModel() { }
    }
}
