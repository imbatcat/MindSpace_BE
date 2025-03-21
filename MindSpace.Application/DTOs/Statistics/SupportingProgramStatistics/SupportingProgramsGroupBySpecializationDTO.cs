namespace MindSpace.Application.DTOs.Statistics.SupportingProgramStatistics
{
    public class SupportingProgramsGroupBySpecializationDTO
    {
        public int SchoolId { get; set; }
        public int TotalSupportingProgramCount { get; set; }
        public List<SupportingProgramPairDTO> KeyValuePairs { get; set; }
    }
}
