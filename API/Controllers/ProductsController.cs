using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _Context;

        public ProductsController(StoreContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Product>>> GetProduct()
        {
            var products = await _Context.Products.ToListAsync();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _Context.Products.FindAsync(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
