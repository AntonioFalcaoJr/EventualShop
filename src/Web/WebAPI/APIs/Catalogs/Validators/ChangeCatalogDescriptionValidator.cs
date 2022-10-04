using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ChangeCatalogDescriptionValidator : AbstractValidator<Requests.ChangeCatalogDescription>
{
    public ChangeCatalogDescriptionValidator()
    {
        RuleFor(request => request.Description)
            .NotEmpty();

        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}

