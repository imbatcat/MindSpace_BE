using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Interfaces.Services.SignalR
{
    public interface ISignalRNotification
    {
        public Task NotifyScheduleStatus(PsychologistSchedule schedule);
    }
}
