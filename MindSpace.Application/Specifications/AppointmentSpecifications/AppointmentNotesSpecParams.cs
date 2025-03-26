namespace MindSpace.Application.Specifications.AppointmentSpecifications
{
    public class AppointmentNotesSpecParams : BasePagingParams
    {
        public int AccountId { get; set; }
        public bool? IsNoteShown { get; set; }
    }
}
