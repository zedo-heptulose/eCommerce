using eCommerce.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        public ReadOnlyCollection<ShoppingCart> carts;

        public ShoppingCart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    return new ShoppingCart();
                }       
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }


        private ShoppingCartService() 
        { 
            List<ShoppingCart> cart_list = new List<ShoppingCart>();
            cart_list.Add(new ShoppingCart {Id = 1, Contents = new List<Product>() });
            carts = new ReadOnlyCollection<ShoppingCart>(cart_list);
        }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        //public ShoppingCart AddOrUpdate(ShoppingCart c)
        //{
        //    //TODO: Someone do this.
        //}

        public void AddToCart(Product newProduct) //probably want to change this to work by ID
        {
            if (Cart == null)
            {
                return;
            }
            if (Cart.Contents == null)
            {
                return ;
            }

            //just looking at the code, the issue seems to be that the products in the inventory and cart
            //are being obtained by value and not by reference.
            Product existingProduct = Cart?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            Product inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if(inventoryProduct == null)
            {
                return;
            }

            Product productToAdd = new Product(newProduct);///here we make a copy so that product is added to shopping cart by value

            int? temp_quantity = productToAdd.Quantity;

            inventoryProduct.Quantity -= temp_quantity;
            //inventoryProduct and newProduct are the same thing; they are passed by reference
            

            if (existingProduct == null)
            {
                Cart?.Contents.Add(productToAdd);
            }
            else
            {
                int? old_quant = existingProduct.Quantity;
                existingProduct.Quantity += temp_quantity;

                if (existingProduct.Quantity == old_quant)
                {
                    return;
                }
            }
            

        }

    }
}
