using FluentValidation;

namespace WebAPI.APIs.Orders.Validators;

public class CancelOrderValidator : AbstractValidator<Requests.CancelOrder>
{
    public CancelOrderValidator()
    {
        RuleFor(account => account.OrderId)
            .NotNull()
            .NotEmpty();
    }
}