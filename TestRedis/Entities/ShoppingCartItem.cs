using System;

namespace TestRedis.Entities
{
    public class ShoppingCartItem
    {
        public string? RedisId { get; set;} = Guid.NewGuid().ToString();
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
