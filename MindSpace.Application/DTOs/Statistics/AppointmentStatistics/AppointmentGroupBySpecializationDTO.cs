namespace MindSpace.Application.DTOs.Statistics.AppointmentStatistics
{
    public class AppointmentGroupBySpecializationDTO
    {
        public int SchoolId {  get; set; }
        public int TotalAppointmentCount { get; set; }
        public List<AppointmentPairDTO> KeyValuePairs { get; set; }
    }
}
