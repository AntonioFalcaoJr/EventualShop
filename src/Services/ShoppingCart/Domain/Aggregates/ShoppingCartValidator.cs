using System;
using System.Linq;
using Domain.Abstractions.Validators;
using Domain.Entities.Customers;
using Domain.Entities.ShoppingCartItems;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.DebitCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using FluentValidation;

namespace Domain.Aggregates;

public class ShoppingCartValidator : EntityValidator<ShoppingCart, Guid>
{
    public ShoppingCartValidator()
    {
        RuleFor(cart => cart.Customer)
            .SetValidator(new CustomerValidator());

        RuleForEach(cart => cart.Items)
            .NotNull()
            .SetValidator(new ShoppingCartItemValidator());

        When(cart => cart.Items.Any(), () =>
        {
            RuleFor(cart => cart.Total)
                .GreaterThan(0);
        });

        RuleForEach(cart => cart.PaymentMethods)
            .SetInheritanceValidator(validator =>
            {
                validator.Add(new CreditCardPaymentMethodValidator());
                validator.Add(new DebitCardPaymentMethodValidator());
                validator.Add(new PayPalPaymentMethodValidator());
            });
    }
}