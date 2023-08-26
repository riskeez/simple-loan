using FastEndpoints;
using SimpleLoan.Application;
using SimpleLoan.Application.DTO;
using SimpleLoan.Application.Requests;
using SimpleLoan.Domain.Entities;
using SimpleLoan.Domain.Repositories;

namespace SimpleLoan.API.Endpoints;

/// <summary>
/// Gets stored payment plan
/// </summary>
public class GetPlanEndpoint : Endpoint<GetPlan, PaymentPlanDto>
{
    public override void Configure()
    {
        Get("/plan/{planId:int}");
        AllowAnonymous();
    }
    
    private readonly IPaymentPlanRepository _repository;

    public GetPlanEndpoint(IPaymentPlanRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(GetPlan request, CancellationToken cancellationToken)
    {
        PaymentPlan? plan = await _repository.GetAsync(request.PlanId, cancellationToken);
        if (plan == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        await SendOkAsync(plan.AsDto(), cancellationToken);
    }
}