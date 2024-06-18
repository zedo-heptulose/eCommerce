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
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel SelectedProduct { get; set; }

        public void UpdateProduct()
        {
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }
        
        public InventoryViewModel() 
        { 
        
        }
    }
}
