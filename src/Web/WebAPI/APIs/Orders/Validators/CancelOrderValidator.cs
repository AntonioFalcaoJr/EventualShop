using FluentValidation;

namespace WebAPI.APIs.Orders.Validators;

public class CancelOrderValidator : AbstractValidator<Commands.CancelOrder>
{
    public CancelOrderValidator()
    {
        RuleFor(account => account.OrderId)
            .NotNull()
            .NotEmpty();
    }
}