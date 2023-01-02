using Domain.Entities.CartItems;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.Addresses;
using FluentValidation;

namespace Domain.Aggregates;

public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
{
    public ShoppingCartValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty();
        
        RuleForEach(cart => cart.Items)
            .NotNull()
            .SetValidator(new CartItemValidator());

        When(cart => cart.Items.Any(), () =>
        {
            RuleFor(cart => cart.Total)
                .GreaterThan(0);
        });

        RuleForEach(cart => cart.PaymentMethods)
            .SetValidator(new PaymentMethodValidator());

        When(cart => cart.BillingAddress is not null, () =>
        {
            RuleFor(cart => cart.BillingAddress)
                .SetValidator(new AddressValidator()!);
        });

        When(cart => cart.ShippingAddress is not null, () =>
        {
            RuleFor(cart => cart.ShippingAddress)
                .SetValidator(new AddressValidator()!);
        });
    }
}