using FluentAssertions;
using Microsoft.Extensions.Logging;
using SimpleLoan.Application.Exceptions;
using SimpleLoan.Application.Finance;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.UnitTests.Application;

public class LoanCalculatorFactoryTests
{
    [Fact]
    public void Get_ShouldReturnExpectedObject()
    {
        // Arrange
        string type = "standard";
        var rateProvider = Substitute.For<ILoanRateProvider>();
        var logger = Substitute.For<ILogger<LoanStandardCalculator>>();
        var serviceProvider = Substitute.For<IServiceProvider>();
        var expectedCaluclator = new LoanStandardCalculator(rateProvider, logger);

        var types = new Dictionary<string, Type>();
        types[expectedCaluclator.Type] = expectedCaluclator.GetType();
        
        serviceProvider.GetService(typeof(LoanStandardCalculator))
            .Returns(expectedCaluclator);
        var factory = new LoanCalculatorFactory(types, serviceProvider);
        
        // Act
        var calculator = factory.Get(type);
        
        // Assert
        calculator.Should().BeOfType<LoanStandardCalculator>();
    }

    [Fact]
    public void Get_WithInvalidType_ShouldThrowInvalidLoanTypeException()
    {
        var types = new Dictionary<string, Type>();
        var serviceProvider = Substitute.For<IServiceProvider>();
        var factory = new LoanCalculatorFactory(types, serviceProvider);
        string type = "invalid_type";

        var act = () => factory.Get(type);

        act.Should().Throw<InvalidLoanTypeException>();
    }
}