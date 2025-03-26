using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;
using System.Linq.Expressions;

namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecification : BaseSpecification<Appointment>
    {
        public enum StringParameterType
        {
            StudentEmail,
            SessionId,
            PsychologistEmail
        }

        public enum IntParameterType
        {
            MeetingRoomId,
            AppointmentId,
        }
        /// <summary>
        /// Specification for filtering appointments by student email, session ID, or appointment reference.
        /// </summary>
        /// <param name="value">The string value to filter by</param>
        /// <param name="parameterType">Specifies the type of the string parameter</param>
        public AppointmentSpecification(string value, StringParameterType parameterType)
            : base(GetFilterExpression(value, parameterType))
        {
            AddInclude(a => a.PsychologistSchedule);
            AddInclude(a => a.Psychologist);
            AddInclude(a => a.Student);
        }

        public AppointmentSpecification(int appointmentId)
            : base(x => x.Id.Equals(appointmentId))
        {
        }

        public AppointmentSpecification(AppointmentNotesSpecParams specParams)
            : base(x =>
                (x.StudentId.Equals(specParams.AccountId) || x.PsychologistId.Equals(specParams.AccountId))
                && (!specParams.IsNoteShown.HasValue || x.IsNoteShown == specParams.IsNoteShown)
                && (x.Status == AppointmentStatus.Success)
                && (!string.IsNullOrEmpty(x.NotesTitle))
            )
        {
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }

        /// <summary>
        /// Specification for filtering appointments by student, psychologist and schedule IDs.
        /// Only returns pending appointments and includes the related PsychologistSchedule.
        /// </summary>
        /// <param name="studentId">The student ID to filter by</param>
        /// <param name="psychologistId">The psychologist ID to filter by</param>
        /// <param name="scheduleId">The schedule ID to filter by</param>
        public AppointmentSpecification(int studentId, int psychologistId, int scheduleId, int ExpireTimeInMinutes)
            : base(
                a => a.StudentId == studentId &&
                a.PsychologistId == psychologistId &&
                a.PsychologistScheduleId == scheduleId &&
                a.Status == AppointmentStatus.Pending &&
                a.CreateAt >= DateTime.Now.AddMinutes(-ExpireTimeInMinutes)
            )
        {
        }

        /// <summary>
        /// Specification for filtering appointments by appointment ID, meeting room ID, or student ID.
        /// </summary>
        /// <param name="appointmentId">The appointment ID to filter by</param>
        /// <param name="parameterType">Specifies the type of the integer parameter</param>
        public AppointmentSpecification(int appointmentId, IntParameterType parameterType)
            : base(GetFilterExpression(appointmentId, parameterType))
        {
        }

        public AppointmentSpecification(int studentId, int psychologistId, int scheduleId)
            : base(
                a => a.StudentId == studentId &&
                a.PsychologistId == psychologistId &&
                a.PsychologistScheduleId == scheduleId &&
                a.Status == AppointmentStatus.Pending
            )
        {
            AddInclude(a => a.PsychologistSchedule);
        }

        private static Expression<Func<Appointment, bool>>? GetFilterExpression(int appointmentId, IntParameterType parameterType)
        {
            return parameterType switch
            {
                IntParameterType.AppointmentId => a => a.Id == appointmentId,
                IntParameterType.MeetingRoomId => a => a.MeetingRoomId == appointmentId,
                _ => null
            };
        }
        private static Expression<Func<Appointment, bool>> GetFilterExpression(string value, StringParameterType parameterType)
        {
            return parameterType switch
            {
                StringParameterType.StudentEmail => a => a.Student.Email == value,
                StringParameterType.SessionId => a => a.SessionId == value,
                StringParameterType.PsychologistEmail => a => a.Psychologist.Email == value,
                _ => throw new ArgumentException($"Unsupported parameter type: {parameterType}", nameof(parameterType))
            };
        }

        public AppointmentSpecification(int schoolId, DateOnly? startDate, DateOnly? endDate) : base(
            a => a.Student.SchoolId == schoolId
            && (!startDate.HasValue || a.PsychologistSchedule.Date >= startDate)
            && (!endDate.HasValue || a.PsychologistSchedule.Date <= endDate)
            )
        {

            AddInclude(a => a.Specialization);
            AddInclude(a => a.PsychologistSchedule);
            AddInclude(a => a.Student);
        }

        public AppointmentSpecification(AppointmentSpecParams specParams, string userEmail) : base(
            x => (
                x.Student!.Email.Equals(userEmail)
            && (String.IsNullOrEmpty(specParams.PsychologistName) || x.Psychologist.FullName.ToLower().Contains(specParams.PsychologistName.ToLower().Trim()))
            && (specParams.StartDate.CompareTo(x.PsychologistSchedule.Date) <= 0 && specParams.EndDate.CompareTo(x.PsychologistSchedule.Date) >= 0)
            && x.Status.Equals(AppointmentStatus.Success)
            )
        )
        {
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

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
