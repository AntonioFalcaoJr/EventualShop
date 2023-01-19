using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class CreateCartPayloadValidator : AbstractValidator<Payloads.CreateCartPayload>
{
    public CreateCartPayloadValidator()
    {
        RuleFor(payload => payload.CustomerId)
            .NotEmpty();

        RuleFor(payload => payload.Currency)
            .NotEmpty();
    }
}