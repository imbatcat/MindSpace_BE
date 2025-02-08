using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Services.Authentication
{
    public interface IAccessTokenProvider
    {
        string CreateToken(ApplicationUser user, string role);
    }
}