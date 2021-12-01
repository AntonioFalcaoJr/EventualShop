using System;
using System.Linq;
using Domain.Abstractions.Validators;
using Domain.Entities.PaymentMethods.CreditCards;
using Domain.Entities.PaymentMethods.DebitCards;
using Domain.Entities.PaymentMethods.PayPal;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CartItems;
using FluentValidation;

namespace Domain.Aggregates;

public class CartValidator : EntityValidator<Cart, Guid>
{
    public CartValidator()
    {
        RuleFor(cart => cart.UserId)
            .NotEqual(Guid.Empty);

        RuleForEach(cart => cart.Items)
            .NotNull()
            .SetValidator(new CartItemValidator());

        When(cart => cart.Items.Any(), () =>
        {
            RuleFor(cart => cart.Total)
                .GreaterThan(0);
        });

        RuleFor(cart => cart.BillingAddress)
            .SetValidator(new AddressValidator());

        RuleFor(cart => cart.ShippingAddress)
            .SetValidator(new AddressValidator());

        RuleForEach(cart => cart.PaymentMethods)
            .SetInheritanceValidator(validator =>
            {
                validator.Add(new CreditCardPaymentMethodValidator());
                validator.Add(new DebitCardPaymentMethodValidator());
                validator.Add(new PayPalPaymentMethodValidator());
            });
    }
}