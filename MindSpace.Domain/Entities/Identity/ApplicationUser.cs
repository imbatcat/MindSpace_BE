using Microsoft.AspNetCore.Identity;
using MindSpace.Domain.Entities.Constants;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

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

        // 1 Psychologist - 1 User 
        public virtual Psychologist Psychologist { get; set; }

        // 1 SchoolManager - 1 User
        public virtual SchoolManager Manager { get; set; }
    }
}