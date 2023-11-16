using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

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
            .SetValidator(new MoneyValidator())
            .OverridePropertyName(string.Empty);

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}