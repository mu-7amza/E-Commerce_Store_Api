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
    public class AuthenticationService(IIdentityService _identityService , ITokenService _tokenService) : IAuthenticationService
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
            
            var rolesResult = await _identityService.GetRolesAsync(loginDto.Email, ct);

            var token = _tokenService.CreateToken(userResult.Data.Id, userResult.Data.Email, userResult.Data.UserName, rolesResult.Data);

            return Result<UserDto>.OK(new UserDto
            {
                Email = userResult.Data.Email,
                DisplayName = userResult.Data.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
           var result = await _identityService.CreateUserAsync(registerDto, ct);

            if(!result.IsSuccess || result.Data is null)
            {
                return Result<UserDto>.Fail(result.Errors);
            }

            var rolesResult = await _identityService.GetRolesAsync(result.Data.Email, ct);

            var token = _tokenService.CreateToken(result.Data.Id, result.Data.Email, result.Data.UserName, rolesResult.Data);


            return Result<UserDto>.OK(new UserDto
            {
                Email = result.Data.Email,
                DisplayName = result.Data.DisplayName,
                Token = "Token"
            });
        }
    }
}
