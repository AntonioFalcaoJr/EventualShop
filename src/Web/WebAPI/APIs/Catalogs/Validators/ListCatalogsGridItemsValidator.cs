using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ListCatalogsGridItemsValidator : AbstractValidator<Query.ListCatalogsGridItems>
{
    public ListCatalogsGridItemsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);
    }
}