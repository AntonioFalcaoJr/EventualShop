using FluentValidation;

namespace WebAPI.DataTransferObjects.Catalogs.Validators;

public class CreateCatalogValidator : AbstractValidator<Requests.CreateCatalog>
{
    public CreateCatalogValidator()
    {

    }
}