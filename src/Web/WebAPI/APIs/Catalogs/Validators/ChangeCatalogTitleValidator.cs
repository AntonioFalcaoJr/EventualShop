using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ChangeCatalogTitleValidator : AbstractValidator<Requests.ChangeCatalogTitle>
{
    public ChangeCatalogTitleValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty();
        
        RuleFor(request => request.CatalogId)
            .NotEmpty();
    }
}
