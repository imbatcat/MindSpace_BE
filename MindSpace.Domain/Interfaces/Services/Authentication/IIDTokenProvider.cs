using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Interfaces.Services.Authentication
{
    public interface IIDTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}