using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

public class StartShoppingValidator : AbstractValidator<Commands.StartShopping>
{
    public StartShoppingValidator()
    {
        RuleFor(request => request.CustomerId)
            .NotNull()
            .NotEmpty();
    }
}