using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Library.Models;
using Microsoft.OpenApi.Any;
namespace eCommerce.API.Database;

public class Filebase
{
    private string _root;

    private static Filebase _instance;
    public static Filebase Current
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Filebase();
            }
            return _instance;
        }
    }
    private Filebase()
    {
        _root = @"C:\temp\Products";
    }

    public int? NextId
    {
        get
        {
            if (!Products.Any()) return 1;

            return Products.Select(x => x.Id).Max() + 1;
        }
    }

    public Product AddOrUpdate(Product p)
    {
        //set up a new Id if one doesn't already exist
        if (p.Id == null || p.Id <= 0)
        {
            p.Id = NextId;
        }
        //go to the right place]
        string path = $"{_root}\\{p.Id}.json";

        //if the item has been previously persisted
        if (File.Exists(path))
        {
            //blow it up
            File.Delete(path);
        }
        //write the file
        try
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(p));
            return p;
        }
        catch
        {
            return new Product();
        }
            //return the item, which now has an id
        
    }
    public List<Product> Products
    {
        get
        {
            var root = new DirectoryInfo(_root);
            var prods = new List<Product>();
            foreach (var productFile in root.GetFiles())
            {
                var product =
                JsonConvert.DeserializeObject <Product>(File.ReadAllText(productFile.FullName));
                prods.Add(product);
            }
            return prods;
        }
    }
    public Product Delete(int id)
    {
        //go to the right place]
        string path = $"{_root}\\{id}.json";
        //if the item has been previously persisted
        if (File.Exists(path))
        {
            //blow it up
            Product p = JsonConvert.DeserializeObject<Product>(File.ReadAllText(path));
            File.Delete(path);
            return p;
        }
        return new Product();
    }
}