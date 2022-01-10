using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Entities;
using server.Repositories;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductService productService;

        public ProductsController(IProductRepository productRepository, IProductService productService)
        {
            this.productRepository = productRepository;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts([FromQuery] decimal? discount, [FromQuery] string filter)
        {
            var products = await productRepository.LoadAll();
        
            if (!string.IsNullOrEmpty(filter))
            {
                products = productService.FilterByCategory(products, filter);
            }
        
            if (discount != null)
            {
                return productService.ApplyDiscount(products, discount.Value);
            }
        
            return products;
        }

        [HttpPut]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await productRepository.Store(product);
            return Accepted();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetByProductId(Guid id)
        {
            var product = await productRepository.Load(id);
            return product;
        }

        [HttpPost("{id}")]
        public async Task<Product> UpdateProduct(Guid id, [FromBody] Product product)
        {
            var updatedProduct = await productRepository.Update(id, product);
            return updatedProduct;
        }
    }
}
