using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanRateProvider : ILoanRateProvider
{
    public Task<decimal> GetRateAsync(string type, CancellationToken cancellationToken)
    {
        // Read config
        decimal rate = 3.5m;
        
        return Task.FromResult(rate);
    }
}