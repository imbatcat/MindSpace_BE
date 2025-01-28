using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }


        // 1 Specialization - M Psychologists
        public virtual ICollection<Psychologist> Psychologists { get; set; } = new HashSet<Psychologist>();

        // 1 Specialization - M Appointments
        public virtual ICollection<Appointments.Appointment> Appointments { get; set; } = new HashSet<Appointments.Appointment>();

        // 1 Specialization - M Test Score Ranks
        public virtual ICollection<Tests.TestScoreRank> TestScoreRanks { get; set; } = new HashSet<Tests.TestScoreRank>();
    }
}
