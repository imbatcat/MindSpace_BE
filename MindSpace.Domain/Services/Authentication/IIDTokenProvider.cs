using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Services.Authentication
{
    public interface IIDTokenProvider
    {
        string CreateToken(ApplicationUser user);
    }
}