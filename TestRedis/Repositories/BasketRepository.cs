using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TestRedis.Entities;

namespace TestRedis.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {

            string connection = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);

            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var OldbasketString = await _redisCache.GetStringAsync(basket.UserName);
            if (!string.IsNullOrEmpty(OldbasketString))
            {
                var Oldbasket = JsonConvert.DeserializeObject<ShoppingCart>(OldbasketString);

                foreach (var item in Oldbasket.Items)
                {
                    basket.Items.Add(item);
                }
            }
            ShoppingCart shopping = new ShoppingCart { Items = basket.Items, UserName = basket.UserName };


            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(shopping));

            return await GetBasket(basket.UserName);
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
            return true;
        }

    }
}
