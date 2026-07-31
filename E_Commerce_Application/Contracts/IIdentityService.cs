using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;

namespace E_Commerce_Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email , CancellationToken ct = default); 
        Task<Result<bool>> CheckPasswordAsync(string email, string password , CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default);
    }
}
