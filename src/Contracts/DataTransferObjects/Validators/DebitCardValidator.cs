using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class DebitCardValidator : AbstractValidator<Dto.DebitCard>
{
    public DebitCardValidator()
    {
        RuleFor(card => card.ExpirationDate)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.Number)
            .CreditCard();

        RuleFor(card => card.HolderName)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.SecurityCode)
            .GreaterThan(ushort.MinValue);
    }
}