using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class GetCatalogItemDetailsValidator : AbstractValidator<Requests.GetCatalogItemDetails>
{
    public GetCatalogItemDetailsValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.ItemId)
            .NotEmpty();
    }
}