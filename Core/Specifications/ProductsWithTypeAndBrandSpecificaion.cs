using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecificaion : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecificaion(ProductSpecParams productsParams)
        :base(x=>
        (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower()
        .Contains(productsParams.Search)) &&
            (!productsParams.BrandId.HasValue || x.ProductBrandId == productsParams.BrandId) &&
            (!productsParams.TypeId.HasValue || x.ProductTypeId == productsParams.TypeId)
            )
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
            AddOrderBy(p=>p.Name);
            ApplyPaging(productsParams.Pagesize * (productsParams.Pageindex -1),productsParams.Pagesize);

            if(!string.IsNullOrEmpty(productsParams.Sort))
            {
                switch (productsParams.Sort)
                {
                    case "priceAsc" :
                     AddOrderBy(p=>p.Price);
                     break;
                     case "priceDesc":
                     AddOrderByDescending(p=>p.Price);
                     break;
                    default:
                    AddOrderBy(n=>n.Name);
                    break;
                }
            }
        }

        public ProductsWithTypeAndBrandSpecificaion(int id) : base(x=>x.Id==id)
        { 
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
        }
    }
}