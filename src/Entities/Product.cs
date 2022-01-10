using System;

namespace server.Entities
{
    public record Product
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; }
        public Category Category { get; init; }
        public decimal Price { get; init; }
    }
}