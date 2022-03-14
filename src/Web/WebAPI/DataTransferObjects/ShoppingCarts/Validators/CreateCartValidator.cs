using System;
using FluentValidation;

namespace WebAPI.DataTransferObjects.ShoppingCarts.Validators;

public class CreateCartValidator : AbstractValidator<Requests.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEqual(Guid.Empty);
    }
}