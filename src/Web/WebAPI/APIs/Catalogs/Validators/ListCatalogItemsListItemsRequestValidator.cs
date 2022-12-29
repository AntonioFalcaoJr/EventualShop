using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ListCatalogItemsListItemsRequestValidator : AbstractValidator<Requests.ListCatalogItemsListItems>
{
    public ListCatalogItemsListItemsRequestValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}