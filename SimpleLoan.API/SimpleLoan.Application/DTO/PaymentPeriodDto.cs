namespace SimpleLoan.Application.DTO;

public class PaymentPeriodDto
{
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
    /// Remaining balance after the period
    /// </summary>
    public decimal RemainingLoan { get; set; }

    public decimal ToPay => PrincipalPayment + InterestPayment;
}