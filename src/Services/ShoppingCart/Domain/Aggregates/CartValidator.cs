using System;
using System.Linq;
using Domain.Abstractions.Validators;
using Domain.Entities.CartItems;
using FluentValidation;

namespace Domain.Aggregates
{
    public class CartValidator : EntityValidator<Cart, Guid>
    {
        public CartValidator()
        {
            RuleFor(cart => cart.CustomerId)
                .NotEqual(Guid.Empty);

            RuleForEach(cart => cart.Items)
                .SetValidator(new CartItemValidator());

            When(cart => cart.Items.Any(), () =>
            {
                RuleFor(cart => cart.Total)
                    .GreaterThan(0);
            });
        }
    }
}