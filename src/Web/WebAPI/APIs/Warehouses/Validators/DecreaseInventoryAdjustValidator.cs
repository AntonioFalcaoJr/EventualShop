using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class DecreaseInventoryAdjustValidator : AbstractValidator<Commands.DecreaseInventoryAdjust>
{
    public DecreaseInventoryAdjustValidator()
    {
        RuleFor(request => request.InventoryId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();

        RuleFor(request => request.Quantity)
            .GreaterThan(0);

        RuleFor(request => request.Reason)
            .NotNull()
            .NotEmpty();
    }
}