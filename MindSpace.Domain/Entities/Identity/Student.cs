namespace MindSpace.Domain.Entities.Identity
{
    public class Student : ApplicationUser
    {
        public virtual ApplicationUser User { get; set; }

        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
