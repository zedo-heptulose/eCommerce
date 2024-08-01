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

        public async void RefreshInventory()
        {
            await InventoryServiceProxy.Current.Get();
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

        public List<ProductViewModel> InventoryItems {
        
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
                return ShoppingCartServiceProxy.Current?.Cart?.Contents?.Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel SelectedCartItem { get; set; }

        public int QuantityToAdd { get; set; }

        public decimal PriceBeforeTax
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Cart?.Contents.Select
                    (p => (1 - p.MarkdownPercent / 100.0M) * p.Price * 
                    (p.BOGO ?? false ? Math.Ceiling((Decimal)p.Quantity / 2.0M) : p.Quantity )) 
                    .Sum() ?? 0;
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

        public async void ExecuteAddToCart(ShopViewModel p)
        {
            if (SelectedInventoryItem == null) { return;  }
            if (SelectedInventoryItem.Product == null) { return; }
            ///NEW
            Product? temp = new Product(SelectedInventoryItem.Product); //make copy here, so we can change its quantity
            temp.Quantity = QuantityToAdd;
            ///NEW
            ShoppingCartServiceProxy.Current.AddToCart(temp);

            temp.Quantity = SelectedInventoryItem.Product.Quantity;
            await InventoryServiceProxy.Current.AddOrUpdate(temp);
            RefreshCart();
            RefreshInventory();
            RefreshPrices();
        }

        public ICommand RemoveFromCartCommand { get; set; }

        public async void ExecuteRemoveFromCart(ShopViewModel p)
        {
            if (SelectedCartItem == null) { return;  }
            if (SelectedCartItem.Product == null) { return; }

            Product? temp = new Product(SelectedCartItem.Product);
            temp.Quantity += InventoryServiceProxy.Current?.Products?.FirstOrDefault(c => c?.Id == temp?.Id).Quantity ?? 0;

            ShoppingCartServiceProxy.Current.RemoveFromCart(SelectedCartItem.Product);
            await InventoryServiceProxy.Current.AddOrUpdate(temp);
            RefreshCart();
            RefreshInventory();
            RefreshPrices();

        }

        public ICommand AccessCartCommand {  get; set; }

        public void ExecuteAccessCart(ShopViewModel p)
        {
            ShoppingCartServiceProxy.Current.CartIndex = 0;
        }
       
        public ICommand AccessWishlistCommand { get; set; }

        public void ExecuteAccessWishList(ShopViewModel p)
        {
            ShoppingCartServiceProxy.Current.CartIndex = 1;
        }

        public ICommand CheckOutCommand { get; set; }

        public void ExecuteCheckOut(ShopViewModel p)
        {
            ShoppingCartServiceProxy.Current.EmptyCart();
        }

        public void SetupCommands()
        {
            AddToCartCommand = new Command(
                (p) => ExecuteAddToCart(p as ShopViewModel));
            RemoveFromCartCommand = new Command(
                (p) => ExecuteRemoveFromCart(p as ShopViewModel));
            AccessCartCommand = new Command (
                (p) => ExecuteAccessCart(p as ShopViewModel));
            AccessWishlistCommand = new Command(
                (p) => ExecuteAccessWishList(p as ShopViewModel));
            CheckOutCommand = new Command(
                (p) => ExecuteCheckOut(p as ShopViewModel));
        }

        public ShopViewModel()
        {
            SetupCommands();
        }
        
    }
}
