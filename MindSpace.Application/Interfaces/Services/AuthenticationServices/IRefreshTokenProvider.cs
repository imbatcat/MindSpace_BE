using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Services.AuthenticationServices
{
    public interface IRefreshTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}