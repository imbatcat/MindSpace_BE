using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Domain.Entities.Identity;
using System.Security.Claims;
using System.Text;

namespace MindSpace.Infrastructure.Services.AuthenticationServices
{
    public sealed class IdTokenProvider(IConfiguration configuration) : IIDTokenProvider
    {
        public string CreateToken(ApplicationUser user, string role)
        {
            var jwtSettings = configuration.GetSection("JwtIDTokenSettings");
            string secretKey = jwtSettings["Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                        new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToString()!),
                        new Claim("username", user.UserName!),
                        new Claim("role", role)
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtIDTokenSettings:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = jwtSettings["Issuer"]!,
                Audience = jwtSettings["Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptior);

            return token;
        }
    }
}