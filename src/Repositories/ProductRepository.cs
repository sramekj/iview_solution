using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Entities;

namespace server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> data;

        public ProductRepository()
        {
            Seed();
        }

        public Task<Product> Load(Guid id)
        {
            return Task.FromResult(data.SingleOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Product>> LoadAll()
        {
            return Task.FromResult((IEnumerable<Product>)data);
        }

        public Task Store(Product product)
        {
            data.Add(product);
            return Task.CompletedTask;
        }

        public Task StoreAll(IList<Product> products)
        {
            data = new List<Product>(products);
            return Task.CompletedTask;
        }

        public Task<Product> Update(Guid id, Product product)
        {
            var itemToUpdate = data.SingleOrDefault(item => item.Id == id);
            if (itemToUpdate == null)
            {
                throw new InvalidOperationException($"Could not find product id: {id}");
            }
            var updatedItem = new Product
            {
                Id = id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };
            data.Remove(itemToUpdate);
            data.Add(updatedItem);
            return Task.FromResult(updatedItem);
        }

        private void Seed()
        {
            if (data == null || data.Count == 0)
            {
                data = new List<Product>(new[]
                    {
                        new Product
                        {
                            Name = "Pizza",
                            Category = Category.Food,
                            Price = 10.99M
                        },
                        new Product
                        {
                            Name = "HotDog",
                            Category = Category.Food,
                            Price = 5.99M
                        },
                        new Product
                        {
                            Name = "Burger",
                            Category = Category.Food,
                            Price = 7.25M
                        },
                        new Product
                        {
                            Name = "Jeans",
                            Category = Category.Clothing,
                            Price = 15.99M
                        },
                        new Product
                        {
                            Name = "T-Shirt",
                            Category = Category.Clothing,
                            Price = 13.5M
                        },
                        new Product
                        {
                            Name = "Jacket",
                            Category = Category.Clothing,
                            Price = 24.99M
                        }
                    });
            }
        }
    }
}