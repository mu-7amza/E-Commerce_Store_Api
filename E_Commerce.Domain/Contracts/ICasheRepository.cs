using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Domain.Contracts
{
    public interface ICasheRepository
    {
        Task<string?> GetAsync(string casheKey , CancellationToken ct = default);
        Task SetAsync(string casheKey, string cashValue, TimeSpan timeToLive, CancellationToken ct = default);
    }
}
