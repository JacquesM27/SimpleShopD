using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleShopD.Domain.Services;
using SimpleShopD.Domain.Users.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SimpleShopD.Infrastructure.Services
{
    public sealed class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration) 
            => _configuration = configuration;

        public string Provide(Role role, Guid userId, string email)
        {
            DateTimeOffset expirationDateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(5);
            var expirationDate = expirationDateTimeOffset.UtcDateTime;

            List<Claim> claims =
            [
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Name, userId.ToString()),
                new(ClaimTypes.Role, role),
                new(ClaimTypes.Email, email)
            ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes
                (_configuration.GetSection("jwt:SecretToken").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string GenerateRandomToken()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] tokenData = new byte[64];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
}
