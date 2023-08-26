using SimpleLoan.Domain.Finance;

namespace SimpleLoan.Application.Requests;

/// <summary>
/// Request to get calculation
/// </summary>
public class GetCalculation : ICalculation
{
    public int Period { get; set; }

    public string Type { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
}