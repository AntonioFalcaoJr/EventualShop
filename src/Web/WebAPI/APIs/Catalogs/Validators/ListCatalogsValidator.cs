using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ListCatalogsValidator : AbstractValidator<Requests.ListCatalogs>
{
    public ListCatalogsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThan(0);
    }
}