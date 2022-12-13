using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ListCatalogItemsValidator : AbstractValidator<Requests.ListCatalogs>
{
    public ListCatalogItemsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThan(0);
    }
}