using System.Text.Json;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities.Basket;
using StackExchange.Redis;

namespace E_Commerce_Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection )
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var jsonData = JsonSerializer.Serialize(basket);
            var success = await _database.StringSetAsync(basket.Id, jsonData, timeToLive?? TimeSpan.FromDays(30));
            return success ? basket : null ;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            return await  _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _database.StringGetAsync(basketId);
            if(basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }
    }
}
