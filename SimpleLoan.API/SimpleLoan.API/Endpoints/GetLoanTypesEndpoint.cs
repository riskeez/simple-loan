using FastEndpoints;
using SimpleLoan.Domain.Finance;

namespace SimpleLoan.API.Endpoints;

/// <summary>
/// Gets a collection of available loan types
/// </summary>
public class GetLoanTypesEndpoint : EndpointWithoutRequest<IEnumerable<string>>
{
    private readonly ILoanTypeProvider _loanTypeProvider;

    public override void Configure()
    {
        Get("/loan/types");
        AllowAnonymous();
    }

    public GetLoanTypesEndpoint(ILoanTypeProvider loanTypeProvider)
    {
        _loanTypeProvider = loanTypeProvider;
    }
    
    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var types = await _loanTypeProvider.GetAsync(cancellationToken);
        
        await SendOkAsync(types, cancellationToken);
    }
}