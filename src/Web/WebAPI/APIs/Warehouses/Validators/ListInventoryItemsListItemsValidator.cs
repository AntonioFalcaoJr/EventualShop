using FluentValidation;

namespace WebAPI.APIs.Warehouses.Validators;

public class ListInventoryItemsListItemsValidator : AbstractValidator<Queries.ListInventoryItemsListItems>
{
    public ListInventoryItemsListItemsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(request => request.InventoryId)
            .NotNull();
    }
}