using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Appointments
{
    public class PsychologistSchedule : BaseEntity
    {
        // 1 Psychologist - M PsychologistSchedules
        public int PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }

        // 1 PsychologistSchedule - 1 Appointment
        public virtual Appointment? Appointment {get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public PsychologistScheduleStatus Status { get; set; }
    }
}
