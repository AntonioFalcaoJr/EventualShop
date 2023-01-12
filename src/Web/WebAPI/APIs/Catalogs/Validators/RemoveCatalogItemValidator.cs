using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class RemoveCatalogItemValidator : AbstractValidator<Commands.RemoveCatalogItem>
{
    public RemoveCatalogItemValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty()
            .NotEqual(request => request.CatalogId);
    }
}