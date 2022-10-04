using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class CreateInventoryValidator : AbstractValidator<Requests.CreateInventory>
{
    public CreateInventoryValidator()
    {
        RuleFor(request => request.OwnerId)
            .NotEmpty();
    }
}