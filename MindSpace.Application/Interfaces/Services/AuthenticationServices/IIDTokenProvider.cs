using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Services.AuthenticationServices
{
    public interface IIDTokenProvider
    {
        string CreateToken(ApplicationUser user, string role);
    }
}