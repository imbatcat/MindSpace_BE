using Microsoft.AspNetCore.SignalR;
using MindSpace.Application.DTOs.Notifications;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Notifications;

namespace MindSpace.Infrastructure.Services.SignalR
{
    public class NotificationService : INotificationService
    {
        // ==============================
        // === Fields & Props
        // ==============================

        private readonly IHubContext<NotificationHub, IClientNotification> _hubContext;

        // ==============================
        // === Constructors
        // ==============================
        public NotificationService(IHubContext<NotificationHub, IClientNotification> hubContext)
        {
            _hubContext = hubContext;
        }

        // ==============================
        // === Methods
        // ==============================

        public async Task NotifyAppointmentFailed(string groupRole, AppointmentNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyAppointmentFailed(payload);
        }

        public async Task NotifyAppointmentPending(string groupRole, AppointmentNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyAppointmentPending(payload);
        }

        public async Task NotifyAppointmentSuccess(string groupRole, AppointmentNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyAppointmentSuccess(payload);
        }

        public async Task NotifyPsychologistScheduleBooked(string groupRole, PsychologistScheduleNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyPsychologistScheduleBooked(payload);

        }

        public async Task NotifyPsychologistScheduleFree(string groupRole, PsychologistScheduleNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyPsychologistScheduleFree(payload);

        }

        public async Task NotifyPsychologistScheduleLocked(string groupRole, PsychologistScheduleNotificationResponseDTO payload)
        {
            await _hubContext.Clients.Group(groupRole).NotifyPsychologistScheduleLocked(payload);
        }
    }
}
