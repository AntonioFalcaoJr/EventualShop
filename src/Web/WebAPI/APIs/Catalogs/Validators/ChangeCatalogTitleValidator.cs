using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class ChangeCatalogTitleValidator : AbstractValidator<Requests.ChangeCatalogTitle>
{
    public ChangeCatalogTitleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
        
        RuleFor(x => x.CatalogId)
            .NotEmpty();
    }
}
