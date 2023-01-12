using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class PayPalValidator : AbstractValidator<Dto.PayPal>
{
    public PayPalValidator()
    {
        RuleFor(payPal => payPal.Password)
            .NotNull()
            .NotEmpty();

        RuleFor(payPal => payPal.Email)
            .EmailAddress();
    }
}