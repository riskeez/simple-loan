using Microsoft.Extensions.DependencyInjection;
using SimpleLoan.Application.Finance;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ILoanRateProvider, LoanRateProvider>();
        services.AddSingleton<ILoanTypeProvider, LoanTypeProvider>();
        services.AddSingleton<ILoanCalculatorFactory, LoanCalculatorFactory>();
        services.AddTransient<LoanStandardCalculator>();
        
        return services;
    }
}