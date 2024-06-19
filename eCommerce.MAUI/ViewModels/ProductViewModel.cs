using eCommerce.Library.Models;
using eCommerce.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eCommerce.MAUI.ViewModels
{
    public class ProductViewModel
    {
        

        public Product Product;

        public string? Name
        {
            get
            {
                return Product?.Name ?? string.Empty;
            }

            set
            {
                if (Product != null)
                {
                    Product.Name = value;
                }
            }
        }

        public string? Description
        {
            get
            {
                return Product?.Description ?? string.Empty;
            }
            set
            {
                if (Product != null)
                {
                    Product.Description = value;
                }
            }
        }

        public decimal? Price
        {
            get
            {
                return Product?.Price ?? 0;
            }
            set
            {
                if (Product != null)
                {
                    Product.Price = value;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Product?.Quantity ?? 0;
            }
            set
            {
                if(Product != null)
                {
                    Product.Quantity = value;
                }
            }
        }

        public ICommand EditCommand { get; set; }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p == null) { return; }
            //Shell.Current.GoToAsync($"//")
        }
        
        public void SetupCommands()
        {
            EditCommand = new Command(
                (p) => ExecuteEdit(p as ProductViewModel));
        }

        public ProductViewModel()
        {
            Product = new Product();
            Product.Id = 0;
            SetupCommands();
        }

        public ProductViewModel(Product _product)
        {
            Product = _product;
            SetupCommands();
        }

        public void AddToInventory()
        {
            InventoryServiceProxy.Current.AddOrUpdate(Product);
        }
        //so, you would do this to prevent circular dependencies.
        //still getting a contact from service proxy in InventoryViewModel,
        //but can use this conversion constructor.


        public string? Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
