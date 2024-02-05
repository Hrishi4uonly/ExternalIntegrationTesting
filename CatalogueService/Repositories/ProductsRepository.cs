using System.Collections.Generic;
using CatalogueService.Repositories.Models;

namespace CatalogueService.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly Product[] _products = 
        {
            new()
            {
                ProductId = "1",
                Description = "Iphone15"
            },
            new()
            {
                ProductId = "2",
                Description = "MACBOOK"
            },
            new()
            {
                ProductId = "3",
                Description = "Apple Watch"
            }
        };
        
        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }
    }
}