using eCommerce.Library.Models;
using eCommerce.API.Database;
using Microsoft.AspNetCore.Mvc;
using eCommerce.API.DTO;

namespace eCommerce.API.ECs
{
    //cannot be a singleton
    //cannot violate REST
    public class InventoryEC
    {
        public InventoryEC()
        {          
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return Filebase.Current.Products.Take(100);
        }

        public async Task<IEnumerable<Product>> Search(string query)
        {
            if (query == null) { query = ""; }
            return Filebase.Current.Products.Where(p => 
                (p.Name != null && p.Name.ToUpper().Contains(query.ToUpper()))
                || (p.Description != null && p.Description.ToUpper().Contains(query.ToUpper())))
                .Take(100);
        }

        public async Task<Product?> Delete(int id)
        {
            return Filebase.Current.Delete(id);
        }

        public async Task<Product> AddOrUpdate(Product p)
        {
            if (p == null) { return new Product(); }

            return Filebase.Current.AddOrUpdate(new Product(p));
        }
    }
}
