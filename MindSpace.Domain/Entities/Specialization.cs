using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Domain.Entities
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }


        // 1 Specialization - M Psychologists
        public virtual ICollection<Psychologist> Psychologists { get; set; } = new HashSet<Psychologist>();

        // 1 Specialization - M Appointments
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

        // 1 Specialization - M Tests
        public virtual ICollection<Test> Tests { get; set; } = new HashSet<Test>();
    }
}
