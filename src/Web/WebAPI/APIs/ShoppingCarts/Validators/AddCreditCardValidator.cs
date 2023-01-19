using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCreditCardValidator : AbstractValidator<Commands.AddCreditCard>
{
    public AddCreditCardValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();

        RuleFor(request => request.Payload)
            .SetValidator(new AddCreditCardPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}