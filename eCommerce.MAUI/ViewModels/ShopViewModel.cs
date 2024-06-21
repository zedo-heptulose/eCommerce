using eCommerce.Library.Services;
using eCommerce.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        //need, to implement:
        //At the very least, the inventoryserviceproxy and 
        //the shopping cart service as members

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshInventory()
        {
            NotifyPropertyChanged("InventoryItems");
        }

        public void RefreshCart()
        {
            NotifyPropertyChanged("CartItems");
        }

        public List<ProductViewModel> InventoryItems
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel? SelectedInventoryItem { get; set; }

        public List<ProductViewModel> CartItems
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public int QuantityToAdd { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public void ExecuteAddToCart(ShopViewModel p)
        {
            if (SelectedInventoryItem == null) { return;  }
            if (SelectedInventoryItem.Product == null) { return; }
            ///NEW
            Product? temp = new Product(SelectedInventoryItem.Product); //make copy here, so we can change its quantity
            temp.Quantity = QuantityToAdd;
            ///NEW
            ShoppingCartService.Current.AddToCart(temp);
            RefreshCart();
            RefreshInventory();
        }

        public void SetupCommands()
        {
            AddToCartCommand = new Command(
                (p) => ExecuteAddToCart(p as ShopViewModel));
        }

        public ShopViewModel()
        {
            SetupCommands();
        }
        
    }
}
