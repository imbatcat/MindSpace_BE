using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class Manager : ApplicationUser
    {
        // 1 Manager - 1 User
        public virtual ApplicationUser User { get; set; }

        // 1 Manager - M TestPublications
        public virtual ICollection<TestPublication> TestPublications { get; set; } = new HashSet<TestPublication>();

        // 1 Manager - 1 School
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        // 1 Manager - M SupportingPrograms
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();

        // 1 Manager - M Resources
        public virtual ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
    }
}