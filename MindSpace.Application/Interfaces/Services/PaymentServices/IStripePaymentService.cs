namespace MindSpace.Application.Interfaces.Services.PaymentServices
{
    public interface IStripePaymentService
    {
        public (string sessionId, string sessionUrl) CreateCheckoutSession(decimal sessionPrice, decimal commisionRate);
    }
}