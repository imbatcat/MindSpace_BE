namespace MindSpace.Domain.Entities.Identity
{
    public class SchoolManager : ApplicationUser
    {
        // 1 Psychologist - 1 User 
        public virtual ApplicationUser User { get; set; }


        // 1 Manager - 1 School
        public int SchoolId { get; set; }
        public virtual School School { get; set; }


        // 1 SchoolManager - M SupportingProgram
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();
    }
}