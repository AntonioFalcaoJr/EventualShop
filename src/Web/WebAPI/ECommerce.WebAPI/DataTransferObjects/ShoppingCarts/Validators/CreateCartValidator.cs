using System;
using FluentValidation;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts.Validators;

public class CreateCartValidator : AbstractValidator<Requests.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEqual(Guid.Empty);
    }
}