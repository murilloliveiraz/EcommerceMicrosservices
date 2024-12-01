using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redis;
        public BasketRepository(IDistributedCache redis)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redis.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redis.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));
            return await GetBasket(basket.Username);
        }
        public async Task DeleteBasket(string username)
        {
            await _redis.RemoveAsync(username);
        }
    }
}
