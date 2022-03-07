using System;
using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Entities.CartItems;

public class CartItemValidator : EntityValidator<CartItem, Guid>
{
    public CartItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}