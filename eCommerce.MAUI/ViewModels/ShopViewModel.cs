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

        public InventoryServiceProxy InventoryServiceProxy { get; set; }
        public ShoppingCartService ShoppingCartService { get; set; }
        
        public ShopViewModel() 
        {
            InventoryServiceProxy = InventoryServiceProxy.Current;

            ShoppingCartService = ShoppingCartService.Current;

        }

        
    }
}
