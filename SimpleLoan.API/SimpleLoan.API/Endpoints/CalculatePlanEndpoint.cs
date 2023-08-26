using FastEndpoints;
using SimpleLoan.Application;
using SimpleLoan.Application.DTO;
using SimpleLoan.Application.Requests;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.API.Endpoints;

/// <summary>
/// Calculates payment plan based on provided requests
/// </summary>
public class CalculatePlanEndpoint : Endpoint<GetCalculation, IEnumerable<PaymentPeriodDto>>
{
    public override void Configure()
    {
        Get("/plan");
        AllowAnonymous();
    }
    
    private readonly ILoanCalculatorFactory _calculatorFactory;
    private readonly ILogger<CalculatePlanEndpoint> _logger;

    public CalculatePlanEndpoint(ILoanCalculatorFactory calculatorFactory, ILogger<CalculatePlanEndpoint> logger)
    {
        _calculatorFactory = calculatorFactory;
        _logger = logger;
    }

    public override async Task HandleAsync(GetCalculation request, CancellationToken cancellationToken)
    {
        string loanType = request.Type.Trim().ToLower();
        ILoanCalculator calculator = _calculatorFactory.Get(loanType);
        
        var payments = await calculator.CalculateAsync(request, cancellationToken);
        await SendOkAsync(payments.Select(x => x.AsDto()), cancellationToken);
    }
}