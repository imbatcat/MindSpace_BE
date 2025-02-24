using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.PsychologistScheduleSpecifications
{
    public class PsychologistScheduleSpecParams 
    {
        public int? PsychologistId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public DateOnly? MinDate { get; set; }
        public DateOnly? MaxDate { get; set; }
        public PsychologistScheduleStatus? Status { get; set; }
    }
}
