using System;
using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.PaymentMethods.PayPal;

public class PayPalPaymentMethodValidator : EntityValidator<PayPalPaymentMethod, Guid>
{
    public PayPalPaymentMethodValidator()
    {
        RuleFor(card => card.Password)
            .NotNull()
            .NotEmpty();

        RuleFor(card => card.UserName)
            .NotNull()
            .NotEmpty();
    }
}