using System.ComponentModel;
using System.Runtime.Serialization;

namespace MindSpace.Application.Commons.Constants
{
    public static class BusinessCts
    {
        public static class StripePayment
        {
            /// <summary>
            /// The time in minutes before a checkout session expires
            /// </summary>
            public static readonly int CheckoutSessionExpireTimeInMinutes = 15;

            /// <summary>
            /// The currency used for payment processing
            /// </summary>
            public static readonly string PaymentCurrency = "VND";


            public enum StripeCheckoutSessionStatus
            {
                open,
                expired,
                completed
            }
        }
    }
}