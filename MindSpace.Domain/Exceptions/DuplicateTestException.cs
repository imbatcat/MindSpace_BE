namespace MindSpace.Domain.Exceptions
{
    public class DuplicateTestException : Exception
    {
        public DuplicateTestException(string message) : base(message)
        {
        }
    }
}
