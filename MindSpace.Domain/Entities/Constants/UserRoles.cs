namespace MindSpace.Domain.Entities.Constants
{
    public static class UserRoles
    {
        public const string Student = "Student";
        public const string Parent = "Parent";
        public const string Psychologist = "Psychologist";
        public const string SchoolManager = "SchoolManager";
        public const string Admin = "Admin";
        public static readonly Dictionary<string, string> RoleMap = new()
        {
            { "1", Student },
            { "2", Admin },
            { "3", Psychologist },
            { "4", SchoolManager },
            { "5", Parent }
        };
    }
}
