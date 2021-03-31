using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
         public DbSet<ProductType> ProductTypes { get; set; }
          public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}
