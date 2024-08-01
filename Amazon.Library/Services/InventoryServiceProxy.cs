using eCommerce.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using eCommerce.Library.Utilities;
using System.ComponentModel;
using eCommerce.Library.DTOs;


namespace eCommerce.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Product> products;

        //read
        public ReadOnlyCollection<Product> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            try
            {
                var deserializedResult = JsonConvert.DeserializeObject<List<Product>>(result);

                products = deserializedResult.ToList() ?? new List<Product>();
                return deserializedResult;
            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }

        private int? NextId
        {
            get
            {
                if(!products.Any())
                {
                    return 1;
                }

                return (products.Select(p => p.Id).Max() ?? 0) + 1;
            }
        }

        //create, update
        //sucessfully reached
        public async Task<Product> AddOrUpdate(Product? p)
        {
            var result = await new WebRequestHandler().Post("/Inventory",p);
            return JsonConvert.DeserializeObject<Product>(result);
        }

        public async Task<Product?> Delete(Product? p)
        {
            //if (p == null) { return null; }
            //Product? to_remove = products.Single(c => c.Id == p.Id);

            //if (to_remove == null)
            //{
            //    return null;
            //}
            //products.Remove(to_remove);
            //return to_remove;
            int id = p.Id ?? 0;
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<Product>(response);
            return itemToDelete;
        }

        public async Task<IEnumerable<Product>> Search(Query? query)
        {
            if (query == null)
            {
                return await Get();
            }
            var result = await new WebRequestHandler().Post("/Inventory/Search",query);
            try
            {
                var deserializedResult = JsonConvert.DeserializeObject<List<Product>>(result) ?? new List<Product>();
                products = deserializedResult.ToList() ?? new List<Product>();
                return deserializedResult;
            }
            catch
            {
                return new List<Product>();
            }
        }

        private InventoryServiceProxy()
        {
            products = new List<Product>();
            //TODO: make a web call
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
