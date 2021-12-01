using System;
using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.PaymentMethods.CreditCards;

public class CreditCardPaymentMethodValidator : EntityValidator<CreditCardPaymentMethod, Guid>
{
    public CreditCardPaymentMethodValidator()
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