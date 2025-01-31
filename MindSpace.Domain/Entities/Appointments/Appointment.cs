using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        // 1 Student - M Appointments
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // 1 Psychologist - M Appointments
        public int PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }

        // 1 PsychologistSchedule - 1 Appointment
        public int PsychologistScheduleId { get; set; }
        public PsychologistSchedule PsychologistSchedule { get; set; }

        // 1 Specialization - M Appointments
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        // 1 Appointment - M Payments
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

        public string MeetURL { get; set; }
        public AppointmentStatus Status { get; set; }

    }
}
