using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Interfaces.Services.Authentication
{
    public interface IAccessTokenProvider
    {
        string CreateToken(ApplicationUser user, string role);
    }
}