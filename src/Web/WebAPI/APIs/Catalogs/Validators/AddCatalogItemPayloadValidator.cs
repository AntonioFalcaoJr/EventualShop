using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class AddCatalogItemPayloadValidator : AbstractValidator<Payload.AddCatalogItem>
{
    public AddCatalogItemPayloadValidator()
    {
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