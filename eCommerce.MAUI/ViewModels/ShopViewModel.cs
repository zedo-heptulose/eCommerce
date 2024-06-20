using eCommerce.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopViewModel
    {
        //need, to implement:
        //At the very least, the inventoryserviceproxy and 
        //the shopping cart service as members

        public List<ProductViewModel> InventoryItems
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }


        public List<ProductViewModel> CartItems
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }
       
        
    }
}
