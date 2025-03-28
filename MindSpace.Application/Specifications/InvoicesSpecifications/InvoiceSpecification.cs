using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Application.Specifications.InvoicesSpecifications
{
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

        public InvoiceSpecification(DateTime? startDate, DateTime? endDate, PaymentType? paymentType, bool isIncludedAppointment = false, bool isIncludedPsychologist = false, bool isIncludedStudent = false)
        : base(x =>
            (!startDate.HasValue || x.CreateAt >= startDate)
            && (!endDate.HasValue || x.CreateAt <= endDate)
        && (!paymentType.HasValue || x.PaymentType == paymentType)
        )
        {
            if (isIncludedAppointment) { AddInclude(x => x.Appointment); }
            if (isIncludedPsychologist) { AddInclude("Appointment.Psychologist"); }
            if (isIncludedStudent) { AddInclude("Account.Student"); }
            
        }
    }
}
