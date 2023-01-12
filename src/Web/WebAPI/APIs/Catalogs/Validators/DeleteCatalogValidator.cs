using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class DeleteCatalogValidator : AbstractValidator<Commands.DeleteCatalog>
{
    public DeleteCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}