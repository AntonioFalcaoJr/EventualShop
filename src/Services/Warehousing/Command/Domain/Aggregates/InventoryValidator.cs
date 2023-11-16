using FluentValidation;

namespace Domain.Aggregates;

public class InventoryValidator : AbstractValidator<Inventory>
{
    public InventoryValidator()
    {
        RuleFor(inventory => inventory.Id)
            .NotEmpty();
    }
}