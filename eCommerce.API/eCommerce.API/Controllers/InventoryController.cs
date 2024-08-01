using Microsoft.AspNetCore.Mvc;
using eCommerce.Library.Models;
using eCommerce.API.ECs;
using eCommerce.API.DTO;


namespace eCommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        //DEPENDENCY INJECTION example
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Product>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpDelete("/{id}")]
        public async Task<Product?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost("Search")] 
        public async Task<IEnumerable<Product?>> Get(Query query)
        {
            return await new InventoryEC().Search(query.QueryString ?? string.Empty);
        }

        //can only use FromBody one time
        [HttpPost()]
        public async Task<Product> AddOrUpdate([FromBody] Product p)
        {
            return await new InventoryEC().AddOrUpdate(p);
        }
    }
}  
