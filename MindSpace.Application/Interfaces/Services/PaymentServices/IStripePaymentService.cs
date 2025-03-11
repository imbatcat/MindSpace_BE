namespace MindSpace.Application.Interfaces.Services.PaymentServices
{
    public interface IStripePaymentService
    {
        public string CreateCheckoutSession(decimal sessionPrice, decimal commisionRate);
        public Task ExpireStripeCheckoutSession(string sessionId);
    }
}