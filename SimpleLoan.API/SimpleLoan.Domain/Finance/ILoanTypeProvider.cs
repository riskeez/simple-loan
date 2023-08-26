namespace SimpleLoan.Domain.Finance;

public interface ILoanTypeProvider
{
    Task<IReadOnlyCollection<string>> GetAsync(CancellationToken cancellationToken);
}