using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class AddCatalogItemValidator : AbstractValidator<Requests.AddCatalogItem>
{
    public AddCatalogItemValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotEmpty();

        RuleFor(request => request.Payload)
            .SetValidator(new AddCatalogItemPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}