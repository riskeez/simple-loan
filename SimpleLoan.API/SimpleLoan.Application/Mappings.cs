using SimpleLoan.Application.DTO;
using SimpleLoan.Domain.Entities;

namespace SimpleLoan.Application;

public static class Mappings
{
    public static PaymentPeriodDto AsDto(this PaymentPeriod entity)
    {
        return new PaymentPeriodDto()
        {
            PeriodNo = entity.PeriodNo,
            RemainingLoan = entity.RemainingLoan,
            InterestPayment = entity.InterestPayment,
            PrincipalPayment = entity.PrincipalPayment
        };
    }

    public static PaymentPlanDto AsDto(this PaymentPlan entity)
    {
        return new PaymentPlanDto()
        {
            Id = entity.Id,
            Payments = entity.Payments.Select(x => x.AsDto()).ToList()
        };
    }
}