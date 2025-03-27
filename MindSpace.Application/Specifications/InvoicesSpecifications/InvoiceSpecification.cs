using MindSpace.Application.Specifications.AppointmentSpecifications;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.InvoicesSpecifications
{
    /*
     * public string? TransactionCode { get; set; }
        public string? Provider { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
     */
    public class InvoiceSpecification : BaseSpecification<Invoice>
    {
        public InvoiceSpecification(InvoiceSpecParams specParams)
    : base(x => (!specParams.AppointmentId.HasValue || x.AppointmentId == specParams.AppointmentId)
        && (string.IsNullOrEmpty(specParams.AccountName) || x.Account.FullName.ToLower().Contains(specParams.AccountName.ToLower()))
        && (!specParams.MinAmount.HasValue || x.Amount >= specParams.MinAmount)
        && (!specParams.MaxAmount.HasValue || x.Amount <= specParams.MaxAmount)
        && (string.IsNullOrEmpty(specParams.TransactionCode) || x.TransactionCode.ToLower().Contains(specParams.TransactionCode.ToLower()))
        && (string.IsNullOrEmpty(specParams.Provider) || x.Provider.ToLower().Contains(specParams.Provider.ToLower()))
        && (string.IsNullOrEmpty(specParams.PaymentMethod) || x.PaymentMethod.ToString().ToLower().Contains(specParams.PaymentMethod.ToLower()))
        && (!specParams.PaymentType.HasValue || specParams.PaymentType == x.PaymentType)
        && (string.IsNullOrEmpty(specParams.FromDate.ToString()) || x.TransactionTime >= specParams.FromDate)
        && (string.IsNullOrEmpty(specParams.ToDate.ToString()) || x.TransactionTime <= specParams.ToDate)

    )
        {
            AddInclude(a => a.Account);
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }

        public InvoiceSpecification(InvoiceSpecParams specParams, string userEmail)
        : base(x =>
            x.Account!.Email.Equals(userEmail)
            && (!specParams.AppointmentId.HasValue || x.AppointmentId == specParams.AppointmentId)
            && (!specParams.MinAmount.HasValue || x.Amount >= specParams.MinAmount)
            && (!specParams.MaxAmount.HasValue || x.Amount <= specParams.MaxAmount)
            && (string.IsNullOrEmpty(specParams.TransactionCode) || x.TransactionCode.ToLower().Contains(specParams.TransactionCode.ToLower()))
            && (string.IsNullOrEmpty(specParams.Provider) || x.Provider.ToLower().Contains(specParams.Provider.ToLower()))
            && (!specParams.PaymentType.HasValue || specParams.PaymentType == x.PaymentType)
            && (string.IsNullOrEmpty(specParams.FromDate.ToString()) || x.TransactionTime >= specParams.FromDate)
            && (string.IsNullOrEmpty(specParams.ToDate.ToString()) || x.TransactionTime <= specParams.ToDate)

        )
        {
            AddInclude(a => a.Account);
            AddPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
