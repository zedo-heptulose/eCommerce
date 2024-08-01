using eCommerce.Library.Models;

namespace eCommerce.API.Database

{
    public static class FakeDatabase
    {
        public static int? NextId
        {
            get
            {
                if(!Products.Any()) return 1;

                return Products.Select(x => x.Id).Max() + 1;
            }
        }
        public static List<Product> Products { get; } = new List<Product>
            {
                new Product { Id = 1,Name="Product 1",Price=1.75M, Quantity=3}
                , new Product {Id = 2, Name="Product 2",Price=17.5M, Quantity=5}
                , new Product {Id = 3, Name="Product 3",Price=12.5M, Quantity=3}
                , new Product {Id = 4, Name="Product 4",Price=11.5M, Quantity=2}
            };
    }
}
