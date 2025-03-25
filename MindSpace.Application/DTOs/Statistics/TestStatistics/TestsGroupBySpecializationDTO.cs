namespace MindSpace.Application.DTOs.Statistics.TestStatistics
{
    public class TestsGroupBySpecializationDTO
    {
        public int SchoolId { get; set; }
        public int TotalTestCount { get; set; }
        public List<PairDTO> KeyValuePairs { get; set; }
    }

    public class PairDTO
    {
        public SpecializationDTO Specialization { get; set; }
        public int TestCount { get; set; }
    }
}
