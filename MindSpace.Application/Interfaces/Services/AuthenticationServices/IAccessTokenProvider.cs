using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Services.AuthenticationServices
{
    public interface IAccessTokenProvider
    {
        string CreateToken(ApplicationUser user, string role);
    }
}