using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

public class RevenueStatDTO
{
    public string Date { get; set; }
    public decimal Revenue { get; set; }
    public decimal Profit { get; set; }

    public static RevenueStatDTO MapToDTO(IGrouping<string, Invoice> group, string groupBy)
    {
        var invoices = group.ToList();

        var purchaseInvoices = invoices.Where(i => i.PaymentType == PaymentType.Purchase);
        decimal totalRevenue = GetTotalAmount(purchaseInvoices);

        var refundInvoices = invoices.Where(i => i.PaymentType == PaymentType.Refund);
        decimal refundAmount = GetTotalAmount(refundInvoices);

        var successAppointedInvoices = invoices.Where(i => i.Appointment.Status == AppointmentStatus.Success);
        decimal psychologistSalary = GetTotalSalary(successAppointedInvoices);

        decimal profit = totalRevenue - refundAmount - psychologistSalary;

        return new RevenueStatDTO
        {
            Date = group.Key,
            Revenue = totalRevenue,
            Profit = profit
        };
    }

    private static decimal GetTotalAmount(IEnumerable<Invoice> invoices)
    {
        return invoices.Sum(i => i.Amount);
    }

    private static decimal GetTotalSalary(IEnumerable<Invoice> successInvoices)
    {
        return successInvoices.Sum(i =>
            i.Amount * (1 - i.Appointment.Psychologist.ComissionRate)
        );
    }
}