using ProductCatalogApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Data
{
    public class CatalogSeed
    {
        public static async Task SeedAsync(CatalogContext context)
        {
            if (!context.CatalogBrands.Any())
            {
                context.CatalogBrands.AddRange(GetSeededCatalogBrands());
                await context.SaveChangesAsync();
            }
            if (!context.CatalogTypes.Any())
            {
                context.CatalogTypes.AddRange(GetSeededCatalogTypes());
                await context.SaveChangesAsync();
            }
            if (!context.CatalogItems.Any())
            {
                context.CatalogItems.AddRange(GetSeededCatalogItems());
                await context.SaveChangesAsync();
            }

        }

        static IEnumerable<CatalogBrand> GetSeededCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand(){ Brand = "Brand 1" },
                new CatalogBrand(){ Brand = "Brand 2" },
                new CatalogBrand(){ Brand = "Brand 3" }
            };
        }
        static IEnumerable<CatalogType> GetSeededCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType(){ Type = "Type 1" },
                new CatalogType(){ Type = "Type 2" },
                new CatalogType(){ Type = "Type 3" }
            };
        }
        static IEnumerable<CatalogItem> GetSeededCatalogItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "testUrl" },
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "testUrl" },
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "Test url" },
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "Test url" },
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "Test url" },
                new CatalogItem(){CatalogTypeId = 2, CatalogBrandId = 3, Description = "Description", Name = "Name", Price = 333, PictureUrl = "Test url" },
            };
        }
    }
}
