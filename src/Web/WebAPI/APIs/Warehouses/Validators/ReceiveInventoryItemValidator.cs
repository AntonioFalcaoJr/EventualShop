using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class ReceiveInventoryItemValidator : AbstractValidator<Commands.ReceiveInventoryItem>
{
    public ReceiveInventoryItemValidator()
    {
        RuleFor(request => request.InventoryId)
            .NotEmpty();

        RuleFor(request => request.Cost)
            .GreaterThan(0);

        RuleFor(request => request.Quantity)
            .GreaterThan(0);

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}