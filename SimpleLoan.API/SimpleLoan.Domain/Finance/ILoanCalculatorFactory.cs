namespace SimpleLoan.Domain.Finance;

public interface ILoanCalculatorFactory
{
    ILoanCalculator Get(string type);
}