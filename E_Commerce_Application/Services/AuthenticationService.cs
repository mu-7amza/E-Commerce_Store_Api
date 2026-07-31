using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Identity;

namespace E_Commerce_Application.Services
{
    public class AuthenticationService(IIdentityService _identityService) : IAuthenticationService
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindByEmailAsync(loginDto.Email, ct);
            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Errors);
            }

            var checkPasswordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);

            if(!checkPasswordResult.IsSuccess)
            {
                return Result<UserDto>.Fail(Error.UnAuthorized("Invalid email or password."));
            }

            return Result<UserDto>.OK(new UserDto
            {
                Email = userResult.Data.Email,
                DisplayName = userResult.Data.DisplayName,
                Token = "Token"
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
           var result = await _identityService.CreateUserAsync(registerDto, ct);

            if(!result.IsSuccess || result.Data is null)
            {
                return Result<UserDto>.Fail(result.Errors);
            }

            return Result<UserDto>.OK(new UserDto
            {
                Email = result.Data.Email,
                DisplayName = result.Data.DisplayName,
                Token = "Token"
            });
        }
    }
}
