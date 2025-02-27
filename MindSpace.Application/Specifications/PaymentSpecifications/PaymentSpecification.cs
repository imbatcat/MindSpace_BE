using MindSpace.Domain.Entities.Appointments;

namespace MindSpace.Application.Specifications.PaymentSpecifications;

public class PaymentSpecification : BaseSpecification<Invoice>
{
    public PaymentSpecification(int paymentId) : base(p => p.TransactionCode == paymentId)
    {
    }
}
