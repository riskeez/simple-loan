namespace SimpleLoan.Domain.Finance;

public interface ICalculation
{
    /// <summary>
    /// Period (in months)
    /// </summary>
    public int Period { get; set; }
    
    /// <summary>
    /// Loan Type
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Loan amount
    /// </summary>
    public decimal Amount { get; set; }
}