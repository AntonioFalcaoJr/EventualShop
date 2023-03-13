using Domain.Enumerations;
using Domain.ValueObjects;
using Domain.ValueObjects.Addresses;
using FluentValidation;

namespace Domain.Aggregates;

public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
{
    public ShoppingCartValidator()
    {
        RuleFor(cart => cart.Id)
            .NotEmpty();

        RuleFor(cart => cart.CustomerId)
            .Equal(CustomerId.Undefined);

        RuleFor(cart => cart.BillingAddress)
            .Equal(Address.Undefined);

        RuleFor(cart => cart.ShippingAddress)
            .Equal(cart => cart.BillingAddress);

        RuleFor(cart => cart.Total)
            .Equal(Money.Zero(Currency.Undefined));

        RuleFor(cart => cart.Status)
            .Equal(CartStatus.Empty);
    }
}