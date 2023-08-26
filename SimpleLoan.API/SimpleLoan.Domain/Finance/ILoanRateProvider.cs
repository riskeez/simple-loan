namespace SimpleLoan.Domain.Finance;

// Gets interest rate for the loan type
public interface ILoanRateProvider
{
    Task<decimal> GetRateAsync(string type);
}