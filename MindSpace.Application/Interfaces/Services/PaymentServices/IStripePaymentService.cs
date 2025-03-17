namespace MindSpace.Application.Interfaces.Services.PaymentServices
{
    public interface IStripePaymentService
    {
        public string CreateCheckoutSession(decimal sessionPrice);
        public Task ExpireStripeCheckoutSession(string sessionId);
    }
}