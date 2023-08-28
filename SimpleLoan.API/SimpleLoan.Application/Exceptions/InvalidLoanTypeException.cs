namespace SimpleLoan.Application.Exceptions;

public class InvalidLoanTypeException : AppException
{
    public override string Code => "invalid_type";

    public InvalidLoanTypeException() : base("Invalid loan type.")
    {
    }
}