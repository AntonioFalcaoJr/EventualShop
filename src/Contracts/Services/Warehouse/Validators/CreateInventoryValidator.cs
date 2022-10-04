using FluentValidation;

namespace Contracts.Services.Warehouse.Validators;

public class CreateInventoryValidator : AbstractValidator<Command.CreateInventory>
{
    public CreateInventoryValidator()
    {
        RuleFor(inventory => inventory.Id)
            .NotEmpty();

        RuleFor(inventory => inventory.OwnerId)
            .NotEmpty();
    }
}