using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Interfaces.Services.Authentication
{
    public interface IRefreshTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}