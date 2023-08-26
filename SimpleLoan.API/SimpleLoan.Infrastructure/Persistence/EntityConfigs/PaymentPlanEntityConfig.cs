using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Infrastructure.Persistence.EntityConfigs;

public class PaymentPlanEntityConfig : IEntityTypeConfiguration<PaymentPlan>
{
    public void Configure(EntityTypeBuilder<PaymentPlan> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LoanType)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(b => b.Payments)
            .WithOne(b => b.PaymentPlan)
            .HasForeignKey(b => b.PaymentPlanId)
                .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}