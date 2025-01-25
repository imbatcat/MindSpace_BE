using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.SupportingPrograms
{
    public class SupportingProgramHistory : BaseEntity
    {
        // Fields
        public DateTime JoinedAt { get; set; }


        // M Students - M Supporting Programs
        public int SupportingProgramId { get; set; }
        public int StudentId { get; set; }
        public SupportingProgram SupportingProgram { get; set; }
        public Student Student { get; set; }
    }
}
