namespace SimpleLoan.Domain.Entities;

public class PaymentPlan
{
    public PaymentPlan()
    {
    }
    
    public PaymentPlan(int id, string type, IEnumerable<PaymentPeriod>? payments = null)
    {
        Id = id;
        LoanType = type;
        CreatedAt = DateTime.UtcNow;
        Payments = new List<PaymentPeriod>(payments ?? Enumerable.Empty<PaymentPeriod>());
    }
    
    /// <summary>
    /// Plan Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Loan Type
    /// </summary>
    public string LoanType { get; set; }
    
    /// <summary>
    /// Created At
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Period Payments
    /// </summary>
    public IReadOnlyCollection<PaymentPeriod> Payments { get; set; }
}