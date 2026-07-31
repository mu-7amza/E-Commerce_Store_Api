using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{

    public class AuthController : ApiBaseController
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var result = await _authService.LoginAsync(loginDto);
            return ToActionResult(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return ToActionResult(result);
        }
    }
}
