using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class DeleteCatalogValidator : AbstractValidator<Requests.DeleteCatalog>
{
    public DeleteCatalogValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}