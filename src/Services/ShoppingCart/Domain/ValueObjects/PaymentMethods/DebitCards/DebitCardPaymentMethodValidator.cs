using System;
using FluentValidation;

namespace Domain.ValueObjects.PaymentMethods.DebitCards;

public class DebitCardPaymentMethodValidator : AbstractValidator<DebitCardPaymentMethod>
{
    public DebitCardPaymentMethodValidator()
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