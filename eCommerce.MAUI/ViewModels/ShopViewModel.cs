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

        public void RefreshPrices()
        {
            NotifyPropertyChanged("PriceBeforeTax");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("PriceAfterTax");
        }

        public string InventorySearchString {  get; set; }

        public List<ProductViewModel> InventoryItems
        {
            get
            {
                if (InventorySearchString == null)
                {
                    return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                        ?? new List<ProductViewModel>();
                }
                else
                {
                    return InventoryServiceProxy.Current?.Products?.ToList().Where(c => c.Name.ToUpper().Contains(InventorySearchString.ToUpper())).Select(c => new ProductViewModel(c)).ToList()
                        ?? new List<ProductViewModel>();
                }
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

        public ProductViewModel SelectedCartItem { get; set; }

        public int QuantityToAdd { get; set; }

        public decimal PriceBeforeTax
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents.Select(p => p.Price * p.Quantity).Sum() ?? 0;
            }
        }

        public decimal Tax
        {
            get
            {
                decimal tax = GlobalVariableService.Current.GlobalVar.TaxRatePercent;
                return PriceBeforeTax * (tax/100);

            }
        }

        public decimal PriceAfterTax
        {
            get
            {
                return PriceBeforeTax + Tax;
            }
        }

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
        }

        public ICommand RemoveFromCartCommand { get; set; }

        public void ExecuteRemoveFromCart(ShopViewModel p)
        {
            if (SelectedCartItem == null) { return;  }
            if (SelectedCartItem.Product == null) { return; }

            Product? temp = new Product(SelectedCartItem.Product);
            temp.Quantity += InventoryServiceProxy.Current?.Products?.FirstOrDefault(c => c?.Id == temp?.Id).Quantity ?? 0;

            ShoppingCartService.Current.RemoveFromCart(SelectedCartItem.Product);
            InventoryServiceProxy.Current.AddOrUpdate(temp);

        }

        public void SetupCommands()
        {
            AddToCartCommand = new Command(
                (p) => ExecuteAddToCart(p as ShopViewModel));
            RemoveFromCartCommand = new Command(
                (p) => ExecuteRemoveFromCart(p as ShopViewModel));
        }

        public ShopViewModel()
        {
            SetupCommands();
        }
        
    }
}
