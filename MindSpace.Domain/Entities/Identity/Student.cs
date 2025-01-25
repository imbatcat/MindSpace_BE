using MindSpace.Domain.Entities.SupportingPrograms;

namespace MindSpace.Domain.Entities.Identity
{
    public class Student : ApplicationUser
    {
        // 1 Student - 1 User
        public virtual ApplicationUser User { get; set; }

        // 1 School - M Students
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        // M Students - M SupportingProgram
        public virtual ICollection<SupportingProgramHistory> SupportingProgramHistory { get; set; } = new HashSet<SupportingProgramHistory>();
    }
}
