using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Domain.Repositories;
using SimpleLoan.Infrastructure.Persistence;

namespace SimpleLoan.Infrastructure.Repositories;

public class PaymentPlanRepository : IPaymentPlanRepository
{
    private readonly PaymentDbContext _dbDbContext;
    private readonly ILogger<PaymentPlanRepository> _logger;

    public PaymentPlanRepository(PaymentDbContext dbDbContext, ILogger<PaymentPlanRepository> logger)
    {
        _dbDbContext = dbDbContext;
        _logger = logger;
    }
    
    public async Task<PaymentPlan?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbDbContext.PaymentPlans
            .Include(x => x.Payments)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(PaymentPlan plan, CancellationToken cancellationToken)
    {
        await _dbDbContext.PaymentPlans.AddAsync(plan, cancellationToken);
        await _dbDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentPlan plan, CancellationToken cancellationToken)
    {
        _dbDbContext.PaymentPlans.Remove(plan);
        await _dbDbContext.SaveChangesAsync(cancellationToken);
    }
}