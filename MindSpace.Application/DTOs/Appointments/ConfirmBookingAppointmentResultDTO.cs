namespace MindSpace.Application.DTOs.Appointments
{
    public class ConfirmBookingAppointmentResultDTO
    {
        public required string SessionId { get; set; }
        public required string SessionUrl { get; set; }
    }
}