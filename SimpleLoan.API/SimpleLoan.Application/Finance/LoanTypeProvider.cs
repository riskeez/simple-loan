using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanTypeProvider : ILoanTypeProvider
{
    // This can be read from config 
    public IReadOnlyCollection<string> Get()
    {
        // read from config
        var types = new List<string>()
        {
            "standard", 
            "car"
        };
        
        return types;
    }
}