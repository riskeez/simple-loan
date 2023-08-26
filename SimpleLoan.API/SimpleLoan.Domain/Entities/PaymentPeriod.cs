namespace SimpleLoan.Domain.Entities;

public class PaymentPeriod
{
    /// <summary>
    /// Period Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Period Number
    /// </summary>
    public int PeriodNo { get; set; }

    /// <summary>
    /// Interest paid this period
    /// </summary>
    public decimal InterestPayment { get; set;  }

    /// <summary>
    /// Main payment paid this period
    /// </summary>
    public decimal PrincipalPayment { get; set; }

    /// <summary>
    /// Remaining loan
    /// </summary>
    public decimal RemainingLoan { get; set; }
    
    /// <summary>
    /// Payment Plan Id
    /// </summary>
    public int PaymentPlanId { get; set; }
    public PaymentPlan PaymentPlan { get; set; }
}