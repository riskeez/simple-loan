using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Domain.Finance;

public interface ILoanCalculator
{
    string Type { get; }
    Task<List<PaymentPeriod>> CalculateAsync(ICalculation loan, CancellationToken cancellationToken);
}