using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.SupportingPrograms;

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


        // 1 Psychologist - M Feedbacks
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        // 1 Psychologist - M PsychologistSchedules
        public virtual ICollection<PsychologistSchedule> PsychologistSchedules { get; set; } = new HashSet<PsychologistSchedule>();

        // 1 Psychologist - M Appointments
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
