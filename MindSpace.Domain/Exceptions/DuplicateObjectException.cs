namespace MindSpace.Domain.Exceptions
{
    public class DuplicateObjectException : Exception
    {
        public DuplicateObjectException(string message) : base(message)
        {
        }
    }
}
