using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandData =
                   await  File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any())
                {
                    var productType =
                   await  File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productType);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any())
                {
                    var product =
                   await  File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(product);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
             var logger = loggerFactory.CreateLogger<StoreContextSeed>();

             logger.LogError(ex.Message,"Error happens in seeding data");
            }
            
        }
    }
}