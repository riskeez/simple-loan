using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Domain.Finance;

public interface ILoanCalculator
{
    Task<List<PaymentPeriod>> CalculateAsync(ICalculation loan, CancellationToken cancellationToken);
}