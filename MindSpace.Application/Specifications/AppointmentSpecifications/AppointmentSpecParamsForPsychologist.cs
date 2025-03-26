namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentSpecParamsForPsychologist : BasePagingParams
    {
        public string? Sort { get; set; } = "dateAsc";
        public DateOnly StartDate { get; set; } = new DateOnly(1, 1, 1);
        public DateOnly EndDate { get; set; } = new DateOnly(9999, 12, 31);
    }
}