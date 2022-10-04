using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class CreditCardValidator : AbstractValidator<Dto.CreditCard>
{
    public CreditCardValidator()
    {
        RuleFor(card => card.Expiration)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.Number)
            .CreditCard();

        RuleFor(card => card.HolderName)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.SecurityNumber)
            .GreaterThan(ushort.MinValue);
    }
}