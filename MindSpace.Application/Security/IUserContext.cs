namespace MindSpace.Application.UserContext
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}