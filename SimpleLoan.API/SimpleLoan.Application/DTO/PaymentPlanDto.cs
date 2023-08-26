namespace SimpleLoan.Application.DTO;

public class PaymentPlanDto
{
    /// <summary>
    /// Plan Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Period Payments
    /// </summary>
    public IEnumerable<PaymentPeriodDto> Payments { get; set; } = Enumerable.Empty<PaymentPeriodDto>();
}