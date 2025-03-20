using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Appointments
{
    public class Invoice : BaseEntity
    {
        // 1 Appointment - M Payments
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public decimal Amount { get; set; }
        public string TransactionCode { get; set; }
        public string Provider { get; set; } // bank name
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentDescription { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime? TransactionTime { get; set; }

        public int AccountId { get; set; }
        public virtual ApplicationUser Account { get; set; }
    }
}
