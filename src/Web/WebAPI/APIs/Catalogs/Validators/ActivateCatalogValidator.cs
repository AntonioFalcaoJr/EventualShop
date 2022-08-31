using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ActivateCatalogValidator : AbstractValidator<Requests.ActivateCatalog>
{
    public ActivateCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}