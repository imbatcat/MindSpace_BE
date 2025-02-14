namespace MindSpace.Domain.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        // ==================================
        // === Props & Fields
        // ==================================

        public string ResourceName { get; private set; }

        // ==================================
        // === Constructors
        // ==================================

        public ResourceAlreadyExistsException(string resourceName)
           : base($"The resource '{resourceName}' already exists.")
        {
            ResourceName = resourceName;
        }
    }
}
