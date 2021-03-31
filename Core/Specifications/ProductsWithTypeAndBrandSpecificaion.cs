using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecificaion : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecificaion()
        {
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
        }

        public ProductsWithTypeAndBrandSpecificaion(int id) : base(x=>x.Id==id)
        { 
            AddInclude(p=>p.ProductBrand);
            AddInclude(p=>p.ProductType);
        }
    }
}