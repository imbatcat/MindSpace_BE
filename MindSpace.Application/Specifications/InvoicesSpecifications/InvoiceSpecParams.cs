using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.InvoicesSpecifications
{
    public class InvoiceSpecParams : BasePagingParams
    {
        public int? AppointmentId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? TransactionCode { get; set; }
        public string? Provider { get; set; }
        public string? PaymentMethod { get; set; }
        public PaymentType? PaymentType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? AccountName { get; set; }
    }
}
