using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Application.Services
{
    internal class TokenServices(IOptions<JWTSettings> options) : ITokenService
    {
        private readonly JWTSettings _settings = options.Value;
        public string CreateToken(string userId, string email, string username, IEnumerable<string> roles)
        {
            // Claims
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Name,username),
            };
            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));   
            
            // Create Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Token
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
                signingCredentials: credintials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

    public class JWTSettings
    {
        public string SecretKey { get; set; } = default!;
        public int ExpirationMinutes { get; set; } 
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }
}
