using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using eCommerce.Library.Services;

namespace eCommerce.MAUI.ViewModels
{
    public class MassImportViewModel
    {
        public MassImportViewModel() { }

        public string Filepath { get; set; }

        public async void ImportFile()
        {
            if (Filepath == null) { return; }
            //do the thing!
            StreamReader sr = new StreamReader(Filepath);
            string line = string.Empty;
            var products = new List<Product>();
            while((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split(['|']);

                products.Add(new Product
                {
                    Name = tokens[0],
                    Price = decimal.Parse(tokens[1]),
                    Quantity = int.Parse(tokens[2]),
                });
            }

            foreach(var product in products)
            {
                await InventoryServiceProxy.Current.AddOrUpdate(product);
            }
        } 
    }
}
