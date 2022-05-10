using Domain.Abstractions.Validators;
using Domain.Entities.CartItems;
using Domain.Entities.PaymentMethods;
using FluentValidation;

namespace Domain.Aggregates;

public class ShoppingCartValidator : EntityValidator<ShoppingCart, Guid>
{
    public ShoppingCartValidator()
    {
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
    }
}