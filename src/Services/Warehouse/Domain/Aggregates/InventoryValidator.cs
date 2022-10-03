using Domain.Abstractions.Validators;
using FluentValidation;

namespace Domain.Aggregates;

public class InventoryValidator : EntityValidator<Inventory, Guid>
{
    public InventoryValidator()
    {
        RuleFor(inventory => inventory.Id)
            .NotEmpty();
    }
}