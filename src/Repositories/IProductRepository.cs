using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Entities;

namespace server.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> LoadAll();
        public Task StoreAll(IList<Product> products);

        public Task<Product> Load(Guid id);

        public Task Store(Product product);

        public Task<Product> Update(Guid id, Product product);
    }
}