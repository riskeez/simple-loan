using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanCalculatorFactory : ILoanCalculatorFactory
{
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<string, Type> _types = new()
    {
        ["standard"] = typeof(LoanStandardCalculator)
    };

    public LoanCalculatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public ILoanCalculator Get(string type)
    {
        var calculator = _serviceProvider.GetService(_types[type]);
        if (calculator == null)
        {
            throw new NotImplementedException();
        }
        return (ILoanCalculator) calculator;
    }
}