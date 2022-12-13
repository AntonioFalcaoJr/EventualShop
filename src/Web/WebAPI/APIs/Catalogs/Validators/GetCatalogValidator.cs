using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class GetCatalogValidator : AbstractValidator<Requests.GetCatalog>
{
    public GetCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}