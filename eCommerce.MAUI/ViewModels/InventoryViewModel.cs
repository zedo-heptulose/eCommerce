using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using eCommerce.Library.Services;
using eCommerce.Library.DTOs;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

       
        private void NotifyPropertyChanged([CallerMemberName] String propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void RefreshProducts()
        {
            await InventoryServiceProxy.Current.Search(SearchQuery);
            NotifyPropertyChanged("Products");
        }

        public void UpdateProduct()
        {
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
            NotifyPropertyChanged(nameof(Products));
        }

        public async void Search()
        {
            await InventoryServiceProxy.Current.Search(SearchQuery);
            NotifyPropertyChanged(nameof(Products));
        }

        public List<ProductViewModel> Products 
        {
            get
            {
                if (SearchString == null)
                {
                    return InventoryServiceProxy.Current?.Products?.ToList().Select(c => new ProductViewModel(c)).ToList()
                        ?? new List<ProductViewModel>();
                }
                else
                {
                    return InventoryServiceProxy.Current.Products?.ToList().Where(c => c?.Name.ToUpper()?.Contains(SearchString.ToUpper()) ?? false).Select(c => new ProductViewModel(c)).ToList()
                        ?? new List<ProductViewModel>();
                }
            }
        }


        public ProductViewModel SelectedProduct { get; set; }

        public string? SearchString {  get; set; }

        public Query SearchQuery
        {
            get
            {
                return new Query{QueryString = SearchString ?? String.Empty};
            }
        }

        public InventoryViewModel() 
        { 
        
        }



    }
}
