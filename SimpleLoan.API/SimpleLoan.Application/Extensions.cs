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

        // Add all calculators
        services.Scan(s => 
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo<ILoanCalculator>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
        
        services.AddSingleton<ILoanCalculatorFactory>(sp =>
        {
            var availableTypes = sp.GetRequiredService<ILoanTypeProvider>().Get();
            var calculators = sp.GetServices<ILoanCalculator>();

            // Add only types available in ILoanTypeProvider
            var types = new Dictionary<string, Type>();
            foreach (var calculator in calculators)
            {
                if (!availableTypes.Contains(calculator.Type))
                {
                    continue;
                }

                types[calculator.Type] = calculator.GetType();
            }
            return new LoanCalculatorFactory(types, sp);
        });
        
        return services;
    }
}