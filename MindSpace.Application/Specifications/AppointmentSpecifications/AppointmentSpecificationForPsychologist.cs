using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecificationForPsychologist : BaseSpecification<Appointment>
    {
        public AppointmentSpecificationForPsychologist(AppointmentSpecParamsForPsychologist specParams, string psychologistEmail) : base(
            x => (
                x.Psychologist!.Email.Equals(psychologistEmail)
                && (specParams.StartDate.CompareTo(x.PsychologistSchedule.Date) <= 0 && specParams.EndDate.CompareTo(x.PsychologistSchedule.Date) >= 0)
                && x.Status.Equals(AppointmentStatus.Success)
            )
        )
        {
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            AddInclude(x => x.Student);
            AddInclude(x => x.PsychologistSchedule);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "dateAsc":
                        AddOrderBy(x => x.PsychologistSchedule.Date); break;
                    case "dateDesc":
                        AddOrderByDescending(x => x.PsychologistSchedule.Date); break;
                }
            }
        }
    }
}