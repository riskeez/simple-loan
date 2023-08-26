using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanTypeProvider : ILoanTypeProvider
{
    public Task<IReadOnlyCollection<string>> GetAsync(CancellationToken cancellationToken)
    {
        // read from config
        var types = new List<string>()
        {
            "standard", 
            "car"
        };
        
        return Task.FromResult<IReadOnlyCollection<string>>(types.AsReadOnly());
    }
}