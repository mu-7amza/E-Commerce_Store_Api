using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contracts;
using StackExchange.Redis;

namespace E_Commerce_Infrastructure.Repository
{
    public class CasheRepository : ICasheRepository
    {
        private readonly IDatabase datebase;
        public CasheRepository(IConnectionMultiplexer connection)
        {
            datebase = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string casheKey, CancellationToken ct = default)
        {
            var value = await datebase.StringGetAsync(casheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string casheKey, string cashValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            await datebase.StringSetAsync(casheKey, cashValue, timeToLive);
        }
    }
}
