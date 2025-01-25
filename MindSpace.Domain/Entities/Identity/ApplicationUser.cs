using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int> //Using int as key type
    {
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserStatus Status { get; set; }

        //Navigation props
        public virtual IEnumerable<TestResponse>? TestResponses { get; set; } = [];

        public int? Student_SchoolId { get; set; }
        public virtual School Student_School { get; set; }
        public int? Manager_SchoolId { get; set; }
        public virtual School Manager_School { get; set; }
        public virtual IEnumerable<Test> Tests { get; set; } = [];

        // 1 Psychologist - 1 User
        public virtual Psychologist Psychologist { get; set; }
    }
}