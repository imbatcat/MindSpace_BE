using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities.Identity
{
    public class SchoolManager : ApplicationUser
    {
        // 1 SchoolManager - 1 User
        public virtual ApplicationUser User { get; set; }

        // 1 SchoolManager - M Tests
        public virtual ICollection<Test> Tests { get; set; } = new HashSet<Test>();

        // 1 SchoolManager - 1 School
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        // 1 SchoolManager - M SupportingPrograms
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();

        // 1 SchoolManager - M Resources
        public virtual ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
    }
}