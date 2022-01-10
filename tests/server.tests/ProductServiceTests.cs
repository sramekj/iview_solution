using System;
using System.Linq;
using NUnit.Framework;
using server.Entities;
using server.Services;

namespace server.tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductService productService;
        private Product[] products;

        [SetUp]
        public void SetUp()
        {
            productService = new ProductService();
            products = new[]
            {
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Category = Category.Food,
                    Name = "some food",
                    Price = 5.5M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Category = Category.Food,
                    Name = "some other food",
                    Price = 6.99M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Category = Category.Clothing,
                    Name = "some clothing",
                    Price = 10.0M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Category = Category.Clothing,
                    Name = "some other clothing",
                    Price = 15.99M
                }
            };
        }

        [Test]
        public void FilterByCategory_WithCorrectFilter_DoesFiltering()
        {
            var filtered = productService.FilterByCategory(products, "Food");
            var expected = new[]
            {
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Category = Category.Food,
                    Name = "some food",
                    Price = 5.5M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Category = Category.Food,
                    Name = "some other food",
                    Price = 6.99M
                }
            };
            
            CollectionAssert.AreEquivalent(expected, filtered);
        }

        [Test]
        public void FilterByCategory_WithCorrectCaseInsensitiveFilter_DoesFiltering()
        {
            var filtered = productService.FilterByCategory(products, "food");
            var expected = new[]
            {
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Category = Category.Food,
                    Name = "some food",
                    Price = 5.5M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Category = Category.Food,
                    Name = "some other food",
                    Price = 6.99M
                }
            };

            CollectionAssert.AreEquivalent(expected, filtered);
        }

        [Test]
        public void FilterByCategory_WithEmptyCollection_ReturnsEmptyCollection()
        {
            var filtered = productService.FilterByCategory(Enumerable.Empty<Product>(), "Food");
            var expected = Enumerable.Empty<Product>();
            CollectionAssert.AreEquivalent(expected, filtered);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("invalid_filter")]
        public void FilterByCategory_WithIncorrectFilter_ReturnsOriginalCollection(string filter)
        {
            var filtered = productService.FilterByCategory(products, filter);
            CollectionAssert.AreEquivalent(products, filtered);
        }

        [Test]
        public void ApplyDiscount_ValidDiscountOnProducts_ReturnsDiscountedProducts()
        {
            var discounted = productService.ApplyDiscount(products, 0.5M);
            var expected = new[]
            {
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Category = Category.Food,
                    Name = "some food",
                    Price = 2.75M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Category = Category.Food,
                    Name = "some other food",
                    Price = 3.495M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Category = Category.Clothing,
                    Name = "some clothing",
                    Price = 5.0M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Category = Category.Clothing,
                    Name = "some other clothing",
                    Price = 7.995M
                }
            };

            CollectionAssert.AreEquivalent(expected, discounted);
        }

        [Test]
        public void ApplyDiscount_100percentDiscountOnProducts_ReturnsFreeProducts()
        {
            var discounted = productService.ApplyDiscount(products, 1M);
            var expected = new[]
            {
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Category = Category.Food,
                    Name = "some food",
                    Price = 0M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Category = Category.Food,
                    Name = "some other food",
                    Price = 0M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Category = Category.Clothing,
                    Name = "some clothing",
                    Price = 0M
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Category = Category.Clothing,
                    Name = "some other clothing",
                    Price = 0M
                }
            };

            CollectionAssert.AreEquivalent(expected, discounted);
        }

        [Test]
        public void ApplyDiscount_ZeroDiscountOnProducts_ReturnsOriginalProducts()
        {
            var discounted = productService.ApplyDiscount(products, 0M);

            CollectionAssert.AreEquivalent(products, discounted);
        }

    }
}
