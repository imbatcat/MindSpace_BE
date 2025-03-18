using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecification : BaseSpecification<Appointment>
    {
        /// <summary>
        /// Specification for filtering appointments by session ID.
        /// </summary>
        /// <param name="sessionId">The session ID to filter by</param>
        public AppointmentSpecification(string sessionId) : base(a => a.SessionId == sessionId)
        {
            AddInclude(a => a.PsychologistSchedule);
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

        public AppointmentSpecification(int appointmentId)
            : base(a => a.Id == appointmentId)
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
    }
}
