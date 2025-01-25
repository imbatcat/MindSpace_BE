using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities
{
    public class Specification : BaseEntity
    {
        public string Name { get; set; }


        // 1 Specification - M Psychologists
        public virtual ICollection<Psychologist> Psychologists { get; set; } = new HashSet<Psychologist>();

        // 1 Specification - M Appointments
        public virtual ICollection<Appointments.Appointment> Appointments { get; set; } = new HashSet<Appointments.Appointment>();
    }
}
