using System;
using FluentValidation;

namespace Domain.ValueObjects.CreditCards
{
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(card => card.Expiration)
                .NotEqual(DateOnly.MinValue)
                .NotEqual(DateOnly.MaxValue);

            RuleFor(card => card.HolderName)
                .NotNull()
                .NotEmpty();

            RuleFor(card => card.Number)
                .NotNull()
                .NotEmpty()
                .CreditCard();

            RuleFor(card => card.SecurityNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}