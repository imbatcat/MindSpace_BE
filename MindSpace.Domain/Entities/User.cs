using Microsoft.AspNetCore.Identity;

namespace MindSpace.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
