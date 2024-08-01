using eCommerce.Library.Models;

namespace eCommerce.API.DTO
   
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Id { get; set; }
        public int? Quantity { get; set; }

        public bool? BOGO { get; set; }

        public decimal? MarkdownPercent { get; set; }


        public ProductDTO()
        {

        }
        public ProductDTO(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            BOGO = p.BOGO;
            MarkdownPercent = p.MarkdownPercent;
        }
    }
}
