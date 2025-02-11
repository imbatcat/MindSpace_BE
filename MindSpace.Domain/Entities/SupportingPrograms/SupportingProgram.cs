using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Owned;
using System.Text.Json.Serialization;

namespace MindSpace.Domain.Entities.SupportingPrograms
{
    public class SupportingProgram : BaseEntity
    {
        // Fields
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PdffileUrl { get; set; }
        public int MaxQuantity { get; set; }
        public Address Address { get; set; }
        public DateTime StartDateAt { get; set; }


        // 1 SchoolManager - M SupportingProgram
        public int SchoolManagerId { get; set; }
        public virtual SchoolManager SchoolManager { get; set; }


        // 1 Psychologist - M SupportingProgram
        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }


        // 1 School - M Supporting Program
        public int SchoolId { get; set; }
        public virtual School School { get; set; }


        // M Students - M Supporting Program
        public virtual ICollection<SupportingProgramHistory> SupportingProgramHistories { get; set; } = new HashSet<SupportingProgramHistory>();
    }
}