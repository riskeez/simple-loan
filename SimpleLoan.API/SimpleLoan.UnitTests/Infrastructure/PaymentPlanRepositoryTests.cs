using FluentAssertions;
using Microsoft.Extensions.Logging;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Infrastructure.Persistence;
using SimpleLoan.Infrastructure.Repositories;

namespace SimpleLoan.UnitTests.Infrastructure;

public class PaymentPlanRepositoryTests : IClassFixture<InMemoryDbFixture>
{
    private readonly InMemoryDbFixture _dbFixture;
    private readonly ILogger<PaymentPlanRepository> _logger;

    public PaymentPlanRepositoryTests(InMemoryDbFixture dbFixture)
    {
        _logger = Substitute.For<ILogger<PaymentPlanRepository>>();
        _dbFixture = dbFixture;
    }

    [Fact]
    public async Task GetAsync_WithExpectedId_ShouldReturnExpectedPlan()
    {
        // Arrange
        int expectedId = 1;
        int expectedPerods = 13;
        decimal expectedSum = 1000.0m;
        
        PaymentPlanRepository repo = GetRepository();
        
        // Act
        var plan = await repo.GetAsync(expectedId, CancellationToken.None);
        
        // Assert
        plan.Should().NotBeNull();
        plan.Id.Should().Be(expectedId);
        plan.Payments.Should().HaveCount(expectedPerods);
        plan.Payments.Select(x => x.PrincipalPayment).Sum().Should().Be(expectedSum);
    }

    [Fact]
    public async Task GetAsync_WithInvalidId_ShouldReturnNull()
    {
        int expectedId = -1;
        
        PaymentPlanRepository repo = GetRepository();

        var plan = await repo.GetAsync(expectedId, CancellationToken.None);
        plan.Should().BeNull();
    }
    
    [Fact]
    public async Task AddAsync_WithValidPlan_ShouldSavePlan()
    {
        // Arrange
        PaymentPlanRepository repo = GetRepository();
        
        List<PaymentPeriod> payments = Enumerable.Range(0, 5)
            .Select(x => new PaymentPeriod()
            {
                PeriodNo = x + 1,
                PrincipalPayment = x * 10,
                InterestPayment = x * 20,
                RemainingLoan = 100
            }).ToList();
        
        var newPlan = new PaymentPlan(0, "standard", payments);
        
        // Act
        await repo.AddAsync(newPlan, CancellationToken.None);
        
        // Assert
        var plan = await repo.GetAsync(newPlan.Id, CancellationToken.None);
        
        plan.Should().NotBeNull();
        plan.Id.Should().Be(newPlan.Id);
        plan.Payments.Should().HaveCount(newPlan.Payments.Count);
    }
    
    [Fact]
    public async Task AddAsync_DuplicatePlanId_ShouldThrow()
    {
        // Arrange
        PaymentPlanRepository repo = GetRepository();
        
        var newPlan = new PaymentPlan(10, "standard");
        
        // Act
        await repo.AddAsync(newPlan, CancellationToken.None);
        var act = async () =>
        {
            await repo.AddAsync(newPlan, CancellationToken.None);
        };
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    private PaymentPlanRepository GetRepository()
    {
        var dbContext = new PaymentDbContext(_dbFixture.ContextOptions);
        return new PaymentPlanRepository(dbContext, _logger);
    }
}