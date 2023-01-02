using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class CreateCatalogValidator : AbstractValidator<Requests.CreateCatalog>
{
    public CreateCatalogValidator()
    {
        RuleFor(request => request.Payload)
            .SetValidator(new CreateCatalogPayloadValidator())
            .OverridePropertyName(string.Empty);
    }
}