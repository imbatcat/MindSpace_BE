using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Application.Interfaces.Services.Authentication
{
    public interface IIDTokenProvider
    {
        string CreateToken(ApplicationUser user, string role);
    }
}