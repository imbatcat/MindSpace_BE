using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class Manager : ApplicationUser
    {
        // 1 Manager - 1 User
        public virtual ApplicationUser User { get; set; }

        // 1 Manager - M Tests
        public virtual ICollection<Test> Tests { get; set; } = new HashSet<Test>();

        // 1 Manager - 1 School
        public int SchoolId { get; set; }

        public virtual School School { get; set; }

        // 1 Manager - M SupportingPrograms
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();
    }
}