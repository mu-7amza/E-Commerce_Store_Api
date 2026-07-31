using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Identity;
using E_Commerce_Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Application.Services
{
    public class IdentityService(UserManager<ApplicationUser> _userManager) : IIdentityService
    {
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return Result<bool>.Fail(Error.UnAuthorized("Invalid email or password"));

            return user is not null && await _userManager.CheckPasswordAsync(user, password)
                ? Result<bool>.OK(true)
                : Result<bool>.Fail(Error.InvalidCredintials("Invalid email or password"));

        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description, ErrorType.Failure)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.OK(
                new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));
        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null ? Result<IdentityUserResult>.OK(
                new IdentityUserResult(user.Id,user.Email,user.UserName,user.DisplayName))
                : Result<IdentityUserResult>.Fail(Error.NotFound("User not found")); 
        }
    }
}
