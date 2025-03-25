namespace MindSpace.Application.DTOs
{
    public class SchoolDTO
    {
        public int? Id { get; set; }
        public string SchoolName { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public AddressDTO Address { get; set; }
        public string Description { get; set; }
        public DateTime JoinDate { get; set; }
        public int? SchoolManagerId { get; set; }
    }
}
