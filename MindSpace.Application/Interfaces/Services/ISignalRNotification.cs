using MindSpace.Domain.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Interfaces.Services
{
    public interface ISignalRNotification
    {
        public Task NotifyScheduleStatus(PsychologistSchedule schedule);
    }
}
