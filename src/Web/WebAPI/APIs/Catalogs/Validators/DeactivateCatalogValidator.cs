using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class DeactivateCatalogValidator : AbstractValidator<Requests.DeactivateCatalog>
{
    public DeactivateCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}