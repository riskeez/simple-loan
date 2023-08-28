using Microsoft.Extensions.DependencyInjection;
using SimpleLoan.Application.Exceptions;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanCalculatorFactory : ILoanCalculatorFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDictionary<string, Type> _types;

    public LoanCalculatorFactory(IDictionary<string, Type> types, IServiceProvider serviceProvider)
    {
        _types = types;
        _serviceProvider = serviceProvider;
    }
    
    public ILoanCalculator Get(string type)
    {
        _types.TryGetValue(type.Trim().ToLower(), out Type? serviceType);

        if (serviceType == null)
        {
            throw new InvalidLoanTypeException();
        }
        
        return (ILoanCalculator)_serviceProvider.GetRequiredService(serviceType);
    }
}