namespace MindSpace.Application.DTOs.Appointments
{
    public class ConfirmBookingAppointmentResult
    {
        public required string SessionId { get; set; }
        public required string SessionUrl { get; set; }
    }
}