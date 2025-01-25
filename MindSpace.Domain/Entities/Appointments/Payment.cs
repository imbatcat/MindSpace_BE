using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Entities.Appointments
{
    public class Payment : BaseEntity
    {
        // 1 User - M Payments
        public int UserId { get; set; }
        public Student User { get; set; }

        // 1 Appointment - M Payments
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        
        public string AccountNo { get; set; }
        public string TransactionCode { get; set; }
        public string Provider { get; set; } // bank name
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentDescription { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime TransactionTime { get; set; }

    }
}
