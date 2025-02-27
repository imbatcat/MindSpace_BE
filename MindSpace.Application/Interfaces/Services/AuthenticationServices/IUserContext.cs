using MindSpace.Application.Commons.Identity;

namespace MindSpace.Application.Interfaces.Services.AuthenticationServices
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
        public int? GetCurrentUserId();
    }
}