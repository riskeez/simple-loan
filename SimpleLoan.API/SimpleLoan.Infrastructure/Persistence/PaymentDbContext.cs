using Microsoft.EntityFrameworkCore;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Infrastructure.Persistence.EntityConfigs;

namespace SimpleLoan.Infrastructure.Persistence;

public class PaymentDbContext : DbContext
{
    public DbSet<PaymentPlan> PaymentPlans => Set<PaymentPlan>();
    public DbSet<PaymentPeriod> PaymentPeriods => Set<PaymentPeriod>();

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new PaymentPeriodEntityConfig());
        modelBuilder.ApplyConfiguration(new PaymentPlanEntityConfig());
    }
}