using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleLoan.Application.Requests;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Infrastructure.Persistence;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}
    
public class DataSeeder : IDataSeeder
{
    private readonly PaymentDbContext _dbContext;
    private readonly ILoanTypeProvider _loanTypeProvider;
    private readonly ILoanCalculatorFactory _calculatorFactory;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(PaymentDbContext dbContext, ILoanTypeProvider loanTypeProvider, ILoanCalculatorFactory calculatorFactory, ILogger<DataSeeder> logger)
    {
        _dbContext = dbContext;
        _loanTypeProvider = loanTypeProvider;
        _calculatorFactory = calculatorFactory;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying db migrations....");
        // Apply migrations
        await _dbContext.Database.MigrateAsync(cancellationToken);
        
        // Seed data if no data in the db
        _logger.LogInformation("Seeding data...");
        if (await _dbContext.PaymentPlans.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Seeding data...");
            return;
        }

        Stopwatch sw = Stopwatch.StartNew();
        
        IReadOnlyCollection<string> loanTypes = await _loanTypeProvider.GetAsync(cancellationToken);
        string loanType = loanTypes.FirstOrDefault() ?? throw new Exception("No loan types have found.");
        ILoanCalculator calculator = _calculatorFactory.Get(loanType);
        
        for (int i = 1; i < 6; i++)
        {
            var loan = new GetCalculation() { Amount = 1000 * i, Period = 12 * i, Type = loanType };
            
            var periods = await calculator.CalculateAsync(loan, cancellationToken);
            
            PaymentPlan plan = new PaymentPlan(0, loan.Type, periods);
            await _dbContext.PaymentPlans.AddAsync(plan, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Seeding data has been completed in {elapsed} ms", sw.ElapsedMilliseconds);
    }
}