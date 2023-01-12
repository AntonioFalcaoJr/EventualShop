using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class DeactivateCatalogValidator : AbstractValidator<Commands.DeactivateCatalog>
{
    public DeactivateCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}