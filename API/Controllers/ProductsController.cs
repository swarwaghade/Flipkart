using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repoProduct;
        private readonly IGenericRepository<ProductBrand> _repoProductBrand;
        private readonly IGenericRepository<ProductType> _repoProductType;

        public ProductsController(IGenericRepository<Product> repoProduct
        ,IGenericRepository<ProductBrand> repoProductBrand,IGenericRepository<ProductType> repoProductType)
        {
            _repoProduct = repoProduct;
            _repoProductBrand = repoProductBrand;
            _repoProductType = repoProductType;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Product>>> GetProduct()
        {
            var spec = new ProductsWithTypeAndBrandSpecificaion();
            var products = await _repoProduct.ListAsync(spec);

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
            var productTypes = await _repoProductType.ListAllAsync();

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
            var productBrand = await _repoProductBrand.ListAllAsync();

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
             var spec = new ProductsWithTypeAndBrandSpecificaion(id);
            var product = await _repoProduct.GetEntityWithSpec(spec);

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
