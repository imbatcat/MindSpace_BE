namespace MindSpace.Domain.Exceptions
{
    public class CreateUserFailedException(string email) : Exception($"Create user {email} failed")
    {
    }
}
