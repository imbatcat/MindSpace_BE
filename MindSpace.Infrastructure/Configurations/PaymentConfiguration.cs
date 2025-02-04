namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Appointments;
using Domain.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments", "dbo");

        // 1 appointment - M payments
        builder.HasOne(p => p.Appointment)
            .WithMany(a => a.Payments)
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(p => p.AccountNo).HasMaxLength(50);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18, 2)");

        builder.Property(p => p.TransactionCode).HasMaxLength(50);

        builder.Property(p => p.Provider).HasMaxLength(50);

        builder.Property(p => p.PaymentDescription).HasMaxLength(1000);

        builder.Property(p => p.TransactionTime).HasColumnType("datetime");

        builder.Property(builder => builder.PaymentType)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<PaymentType>(s));

        builder.Property(builder => builder.Status)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<PaymentStatus>(s));
    }
}
