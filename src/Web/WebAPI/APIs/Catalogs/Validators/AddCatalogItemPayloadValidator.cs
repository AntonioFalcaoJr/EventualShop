using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class AddCatalogItemPayloadValidator : AbstractValidator<Payloads.AddCatalogItem>
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
            .SetValidator(new MoneyValidator())
            .OverridePropertyName(string.Empty);

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}