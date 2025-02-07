using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MindSpace.Domain.Entities.Identity;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MindSpace.Application.Features.Authentication.Services
{
    public class RefreshTokenProvider(IConfiguration configuration)
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