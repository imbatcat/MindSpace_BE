using MediatR;
using Net.payOS.Types;

namespace MindSpace.Application.Features.Payments.Commands.CreatePayment;

public record CreatePaymentCommand : IRequest<CreatePaymentResult>
{
    public int Ammount { get; set; }
    public string Description { get; set; }
    public List<ItemData> Items { get; set; }
    public int AppointmentId { get; set; }
    public int AccountId { get; set; }
}