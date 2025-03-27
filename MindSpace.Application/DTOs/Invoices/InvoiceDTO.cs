namespace MindSpace.Application.DTOs.Invoices
{
    public class InvoiceDTO
    {
        public int? Id { get; set; }
        public int? AppointmentId { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionCode { get; set; }
        public string? Provider { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public string? TransactionTime { get; set; }
        public string? AccountName { get; set; }

    }
}
