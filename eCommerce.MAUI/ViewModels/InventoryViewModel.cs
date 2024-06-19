using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using eCommerce.Library.Services;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProducts()
        {
            NotifyPropertyChanged("Products");
        }

        public void UpdateProduct()
        {
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }


        public List<ProductViewModel> Products 
        {

            get
            {
                return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel SelectedProduct { get; set; }

        public InventoryViewModel() 
        { 
        
        }



    }
}
