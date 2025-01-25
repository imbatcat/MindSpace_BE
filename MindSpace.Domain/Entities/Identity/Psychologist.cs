namespace MindSpace.Domain.Entities.Identity
{
    public class Psychologist : ApplicationUser
    {
        public string Bio { get; set; }
        public float AverageRating { get; set; }
        public decimal SessionPrice { get; set; }
        public decimal ComissionRate { get; set; }


        // 1 Specification - M Psychologists
        public int SpecificationId { get; set; }
        public virtual Specification Specification { get; set; }

        // 1 Psychologist - 1 User 
        public virtual ApplicationUser User { get; set; }

        // 1 Psychologist - M SupportingProgram
        public virtual ICollection<SupportingProgram> SupportingPrograms { get; set; } = new HashSet<SupportingProgram>();
    }
}
