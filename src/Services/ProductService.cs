using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using server.Entities;

namespace server.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<Product> ApplyDiscount(IEnumerable<Product> products, decimal discount)
        {
            var discountedProducts = products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price - p.Price * discount
            });
            return discountedProducts;
        }

        public IEnumerable<Product> FilterByCategory(IEnumerable<Product> products, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter) || !Enum.TryParse<Category>(filter, true, out _))
            {
                return products;
            }
            return products.Where(p => string.Compare(
                    p.Category.ToString(),
                    filter,
                    CultureInfo.InvariantCulture,
                    CompareOptions.OrdinalIgnoreCase) == 0);
        }
    }
}