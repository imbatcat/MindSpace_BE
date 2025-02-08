using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Services.Authentication;
using System.Security.Cryptography;

namespace MindSpace.Application.Services.AuthenticationServices
{
    public class RefreshTokenProvider(IConfiguration configuration) : IRefreshTokenProvider
    {
        public string CreateToken(ApplicationUser user)
        {
            var jwtSettings = configuration.GetSection("JwtRefreshTokenSettings");

            var number = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(number);
            var securityKey = new SymmetricSecurityKey(number);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
                    {
                        { JwtRegisteredClaimNames.Sub, user.Id.ToString() }
                    },
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtRefreshTokenSettings:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = jwtSettings["Issuer"]!,
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}