using eCommerce.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.Services
{
    public class ShoppingCartServiceProxy
    {
        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();

        public List<ShoppingCart> carts;

        public int CartIndex { get; set;} = 0;

        public ShoppingCart Cart //needs to create the thing if it doesn't exist
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    carts = new List<ShoppingCart>();
                }
                while (carts.Count <= CartIndex) 
                { 
                    carts.Add(new ShoppingCart());  
                }
                return carts?[CartIndex]; //now always has a cart for a given index
            } 
        }


        private ShoppingCartServiceProxy() 
        { 
            List<ShoppingCart> cart_list = new List<ShoppingCart>();
            cart_list.Add(new ShoppingCart {Id = 1, Contents = new List<Product>() });
            carts = new List<ShoppingCart>(cart_list);
        }

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
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

        public Product? RemoveFromCart(Product p)
        {
            if (p == null) { return null; }
            Product to_remove;
            try
            {
                to_remove = Cart.Contents.Single(c => c.Id == p.Id);
            }
            catch (InvalidOperationException e)
            {
                return null;
            }

            if (to_remove != null)
            {
                Cart.Contents.Remove(to_remove);
            }
            return to_remove;

        }

        public void EmptyCart()
        {
            carts[CartIndex] = new ShoppingCart();
        }
    }
}
