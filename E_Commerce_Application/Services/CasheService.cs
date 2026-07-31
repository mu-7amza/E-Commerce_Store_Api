using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using E_Commerce_Domain.Contracts;

namespace E_Commerce_Application.Services
{
    public class CasheService : ICasheService
    {
        private readonly ICasheRepository _casheRepository;

        public CasheService(ICasheRepository casheRepository)
        {
            _casheRepository = casheRepository;
        }
        public async Task<string?> GetAsync(string casheKey, CancellationToken ct = default)
        {
            return await _casheRepository.GetAsync(casheKey, ct);
        }

        public Task SetAsync(string casheKey, object cashValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(cashValue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return _casheRepository.SetAsync(casheKey, jsonValue, timeToLive, ct);

        }
    }
}
