using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleLoan.Application.Finance;
using SimpleLoan.Application.Requests;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Domain.Finance;
using SimpleLoan.Infrastructure.Persistence;

namespace SimpleLoan.UnitTests;

public class InMemoryDbFixture : IAsyncLifetime
{
    private DbContextOptions<PaymentDbContext> _dbOptions;

    public DbContextOptions<PaymentDbContext> ContextOptions => _dbOptions;

    public InMemoryDbFixture()
    {
        _dbOptions = new DbContextOptionsBuilder<PaymentDbContext>()
            .UseInMemoryDatabase(databaseName: "in-memory")
            .Options;
    }

    public async Task InitializeAsync()
    {
        CancellationToken token = CancellationToken.None;

        await using var dbContext = new PaymentDbContext(_dbOptions);
        await SeedTestDataAsync(dbContext, token);
        await dbContext.SaveChangesAsync(token);
    }
    
    private async Task SeedTestDataAsync(PaymentDbContext dbContext, CancellationToken cancellationToken = default)
    {
        string type = "standard";
        
        var rateProvider = Substitute.For<ILoanRateProvider>();
        rateProvider.GetRateAsync(type, Arg.Any<CancellationToken>()).Returns(5.0m);
        var logger = Substitute.For<ILogger<LoanStandardCalculator>>();
        
        var calculator = new LoanStandardCalculator(rateProvider, logger);
        for (int i = 1; i < 6; i++)
        {
            var loan = new GetCalculation() { Amount = 1000 * i, Period = 12 * i, Type = type };
            
            var periods = await calculator.CalculateAsync(loan, cancellationToken);
            
            PaymentPlan plan = new PaymentPlan(0, loan.Type, periods);
            await dbContext.PaymentPlans.AddAsync(plan, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DisposeAsync()
    {
        _dbOptions = null;
        return Task.CompletedTask;
    }
}