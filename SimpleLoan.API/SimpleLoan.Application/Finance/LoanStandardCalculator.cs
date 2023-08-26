using Microsoft.Extensions.Logging;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Finance;

public class LoanStandardCalculator : ILoanCalculator
{
    private readonly ILoanRateProvider _rateProvider;
    private readonly ILogger<LoanStandardCalculator> _logger;

    public LoanStandardCalculator(ILoanRateProvider rateProvider, ILogger<LoanStandardCalculator> logger)
    {
        _rateProvider = rateProvider;
        _logger = logger;
    }

    public async Task<List<PaymentPeriod>> CalculateAsync(ICalculation loan, CancellationToken cancellationToken)
    {
        List<PaymentPeriod> result = new();

        decimal rate = await _rateProvider.GetRateAsync(loan.Type) / 100.0m;

        decimal principalPayment = Math.Round(loan.Amount / loan.Period, 2, MidpointRounding.AwayFromZero);
        decimal remainingLoan = loan.Amount;
        for (int i = 0; i < loan.Period + 1; i++)
        {
            decimal periodPrincipalPay = principalPayment;
            remainingLoan = remainingLoan - periodPrincipalPay;
            
            // Special conditions for 1st month
            if (i == 0)
            {
                periodPrincipalPay = 0;
                remainingLoan = loan.Amount;
            }
            // When remaining loan is really small, just add it to period pay and set it to zero
            if (remainingLoan <= 1.0m)
            {
                periodPrincipalPay += remainingLoan;
                remainingLoan = 0;
            }
         
            decimal interestPayment = Math.Round(remainingLoan * rate, 2, MidpointRounding.AwayFromZero);
            result.Add(new()
            {
                PeriodNo = i + 1,
                InterestPayment = interestPayment,
                PrincipalPayment = periodPrincipalPay,
                RemainingLoan = remainingLoan
            });
        }
        
        return result;
    }
}