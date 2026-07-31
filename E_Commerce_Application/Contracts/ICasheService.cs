using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Contracts
{
    public interface ICasheService
    {
        Task<string?> GetAsync(string casheKey, CancellationToken ct = default);
        Task SetAsync(string casheKey, object cashValue, TimeSpan timeToLive, CancellationToken ct = default);
    }
}
