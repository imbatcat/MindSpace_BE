using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Services.Authentication
{
    public interface IRefreshTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}