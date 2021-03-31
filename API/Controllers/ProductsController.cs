using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _Repo;

        public ProductsController(IProductRepository repo)
        {
            _Repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Product>>> GetProduct()
        {
            var products = await _Repo.GetProductsAsync();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        
        [HttpGet("types")]
        public async Task<ActionResult<IList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _Repo.GetProductTypeAsync();

            if (productTypes != null)
            {
                return Ok(productTypes);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IList<ProductBrand>>> GetProductBrands()
        {
            var productBrand = await _Repo.GetProductBrandAsync();

            if (productBrand != null)
            {
                return Ok(productBrand);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _Repo.GetProductByIdAsync(id);

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
