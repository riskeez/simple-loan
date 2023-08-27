namespace SimpleLoan.Domain.Finance;

public interface ILoanTypeProvider
{
    IReadOnlyCollection<string> Get();
}