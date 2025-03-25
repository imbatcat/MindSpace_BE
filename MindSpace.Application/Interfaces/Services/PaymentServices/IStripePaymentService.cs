namespace MindSpace.Application.Interfaces.Services.PaymentServices
{
    public interface IStripePaymentService
    {
        public (string, string) CreateCheckoutSession(decimal sessionPrice);
        public Task ExpireStripeCheckoutSession(string sessionId);
        public Task<string> RetrieveSessionUrlAsync(string sessionId);
    }
}