using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Specifications.PsychologistScheduleSpecifications
{
    public class PsychologistScheduleSpecification : BaseSpecification<PsychologistSchedule>
    {
        // constructor for filter and pagination
        public PsychologistScheduleSpecification(PsychologistScheduleSpecParams specParams)
            : base(x =>
                (!specParams.PsychologistId.HasValue || x.PsychologistId == specParams.PsychologistId) &&
                (!specParams.StartTime.HasValue || x.StartTime >= specParams.StartTime) &&
                (!specParams.EndTime.HasValue || x.EndTime <= specParams.EndTime) &&
                (!specParams.MinDate.HasValue || x.Date >= specParams.MinDate) &&
                (!specParams.MaxDate.HasValue || x.Date <= specParams.MaxDate) &&
                (!specParams.Status.HasValue || x.Status == specParams.Status)
            )
        {
            // Add Paging
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            // Add Sorting
            AddOrderBy(x => x.Date);
            AddOrderBy(x => x.StartTime);
        }
    }
}
