using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.ShoppingCarts.Validators;

public class AddCartItemPayloadValidator : AbstractValidator<Payloads.AddCartItemPayload>
{
    public AddCartItemPayloadValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.InventoryId)
            .NotEmpty();

        RuleFor(request => request.Quantity)
            .GreaterThan(ushort.MinValue);

        RuleFor(request => request.UnitPrice)
            .GreaterThan("0");

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}