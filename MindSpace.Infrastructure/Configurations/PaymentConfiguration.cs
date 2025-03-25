namespace MindSpace.Infrastructure.Configurations;

using Domain.Entities.Appointments;
using Domain.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices", "dbo");

        // 1 appointment - M payments
        builder.HasOne(p => p.Appointment)
            .WithMany(a => a.Invoices)
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(p => p.Account)
            .WithMany()
            .HasForeignKey(p => p.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18, 2)");

        builder.Property(p => p.TransactionCode).HasMaxLength(500);

        builder.Property(p => p.Provider).HasMaxLength(50);

        builder.Property(p => p.PaymentDescription).HasMaxLength(1000);

        builder.Property(p => p.TransactionTime).HasColumnType("datetime");

        builder.Property(builder => builder.PaymentType)
            .HasConversion(
            convertToProviderExpression: s => s.ToString(),
            convertFromProviderExpression: s => Enum.Parse<PaymentType>(s));
    }
}
