using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Domain.Repositories;

public interface IPaymentPlanRepository
{
    Task<PaymentPlan?> GetAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(PaymentPlan plan, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentPlan plan, CancellationToken cancellationToken);
}