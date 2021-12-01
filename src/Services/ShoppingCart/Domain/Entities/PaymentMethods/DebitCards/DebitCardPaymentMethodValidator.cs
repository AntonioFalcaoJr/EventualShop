using System;
using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.PaymentMethods.DebitCards;

public class DebitCardPaymentMethodValidator : EntityValidator<DebitCardPaymentMethod, Guid>
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