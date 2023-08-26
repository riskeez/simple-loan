using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Infrastructure.Persistence.EntityConfigs;

public class PaymentPeriodEntityConfig : IEntityTypeConfiguration<PaymentPeriod>
{
    public void Configure(EntityTypeBuilder<PaymentPeriod> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.PeriodNo)
            .IsRequired();
        
        builder.Property(x => x.PrincipalPayment)
            .IsRequired()
            .HasConversion<double>();
        
        builder.Property(x => x.InterestPayment)
            .IsRequired()
            .HasConversion<double>();
        
        builder.Property(x => x.PrincipalPayment)
            .IsRequired()
            .HasConversion<double>();
    }
}