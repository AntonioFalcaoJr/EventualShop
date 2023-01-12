using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class GetCatalogItemDetailsValidator : AbstractValidator<Query.GetCatalogItemDetails>
{
    public GetCatalogItemDetailsValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}