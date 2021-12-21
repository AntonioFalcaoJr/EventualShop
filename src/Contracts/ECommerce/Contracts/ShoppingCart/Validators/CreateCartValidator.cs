using System;
using FluentValidation;

namespace ECommerce.Contracts.ShoppingCart.Validators;

public class CreateCartValidator : AbstractValidator<Commands.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEqual(Guid.Empty);
    }
}