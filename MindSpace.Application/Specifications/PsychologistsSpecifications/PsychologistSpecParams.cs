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

        public string? Sort;
    }
}
