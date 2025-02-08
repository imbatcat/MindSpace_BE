using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Services.Authentication
{
    public interface IRefreshTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}