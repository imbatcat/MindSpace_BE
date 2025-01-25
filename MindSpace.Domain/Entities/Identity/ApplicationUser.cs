using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int> //Using int as key type
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserStatus Status { get; set; }

        // 1 Student - M Tests
        public virtual IEnumerable<TestResponse>? TestResponses { get; set; } = [];
        public virtual IEnumerable<Test> Tests { get; set; } = [];

        // 1 Psychologist - 1 User
        public virtual Psychologist Psychologist { get; set; }

        // 1 Student - 1 User
        public virtual Student Student { get; set; }

        // 1 SchoolManager - 1 User
        public virtual SchoolManager Manager { get; set; }
    }
}