using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class GetCatalogItemsValidator : AbstractValidator<Requests.GetCatalogItems>
{
    public GetCatalogItemsValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty();
    }
}