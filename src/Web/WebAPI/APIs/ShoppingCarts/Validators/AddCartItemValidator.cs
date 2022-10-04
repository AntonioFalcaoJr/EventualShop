using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCartItemValidator : AbstractValidator<Requests.AddCartItem>
{
    public AddCartItemValidator()
    {
        RuleFor(request => request.CartId)
            .NotEmpty();
        
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.InventoryId)
            .NotEmpty();

        RuleFor(request => request.Quantity)
            .GreaterThan(0);

        RuleFor(request => request.Sku)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.UnitPrice)
            .GreaterThan(0);

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}