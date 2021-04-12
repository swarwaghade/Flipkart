using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _repoProduct;
        private readonly IGenericRepository<ProductBrand> _repoProductBrand;
        private readonly IGenericRepository<ProductType> _repoProductType;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> repoProduct
        , IGenericRepository<ProductBrand> repoProductBrand, IGenericRepository<ProductType> repoProductType
        , IMapper mapper)
        {
            this.mapper = mapper;
            _repoProduct = repoProduct;
            _repoProductBrand = repoProductBrand;
            _repoProductType = repoProductType;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProduct()
        {
            var spec = new ProductsWithTypeAndBrandSpecificaion();
            var products = await _repoProduct.ListAsync(spec);

            if (products != null)
            {
                return Ok(mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products)); 
                // return products.Select(product => new ProductToReturnDto
                // {
                //     Id = product.Id,
                //     Description = product.Description,
                //     Name = product.Name,
                //     Price = product.Price,
                //     PictureUrl = product.PictureUrl,
                //     ProductBrand = product.ProductBrand.Name,
                //     ProductType = product.ProductType.Name
                // }).ToList();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypeAndBrandSpecificaion(id);
            var product = await _repoProduct.GetEntityWithSpec(spec);

            // return new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Description = product.Description,
            //     Name = product.Name,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };
           

            if (product != null)
            {
                return Ok(mapper.Map<Product,ProductToReturnDto>(product)); 
            }
            else
            {
                return NotFound(new ApiResponse(404));
            }
        }
    }
}
