using eCommerce.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Product AddOrUpdate(Product p)
        {
            bool isAdd = false;
            if(p.Id == 0)
            {
                isAdd = true;
                p.Id = NextId;
            }

            if(isAdd == true)
            {
                products.Add(p);
            }
            else
            {
                ///TODO: Implement replacing the item at this point in the list.
            }

            return p;
        }

        public Product? Delete(Product p)
        {
            if (p == null) { return null; }
            Product to_remove;
            try
            {
                to_remove = products.Single(c => c.Id == p.Id);
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
            
            if (to_remove != null)
            {
                products.Remove(to_remove);
            }
            return to_remove;
        }

        private InventoryServiceProxy()
        {
            products = new List<Product>();
           
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
