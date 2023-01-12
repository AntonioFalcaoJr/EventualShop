using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class CreateInventoryValidator : AbstractValidator<Commands.CreateInventory>
{
    public CreateInventoryValidator()
    {
        RuleFor(request => request.OwnerId)
            .NotEmpty();
    }
}