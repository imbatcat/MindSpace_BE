namespace MindSpace.Application.Specifications.ApplicationUserSpecifications
{
    public class ApplicationUserSpecParams : BasePagingParams
    {
        private string _searchName = string.Empty;
        public string? SearchName
        {
            get { return _searchName; }
            set { _searchName = value?.Trim().ToLower() ?? string.Empty; }
        }
        public string? Sort { get; set; }
        public string? Status { get; set; } = "All";
        public string? RoleId { get; set; }
        public int? MinAge { get; set; } = 0;
        public int? MaxAge { get; set; } = 100;
        public int? SchoolId { get; set; }
    }
}