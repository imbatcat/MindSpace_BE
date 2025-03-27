namespace MindSpace.Application.Specifications.PsychologistsSpecifications
{
    public class PsychologistSpecParams : BasePagingParams
    {
        private string _searchName = string.Empty;
        public string? SearchName
        {
            get { return _searchName; }
            set { _searchName = value?.Trim().ToLower() ?? string.Empty; }
        }

        public string? Status { get; set; } = "All";
        public int? SpecializationId { get;set ; }
        public decimal? SessionPriceFrom { get; set; }
        public decimal? SessionPriceTo { get; set; }
        public float? RatingFrom {  get; set; }
        public string? Sort { get; set; }
    }
}
