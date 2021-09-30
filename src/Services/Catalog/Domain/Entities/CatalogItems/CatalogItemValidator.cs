using FluentValidation;

namespace Domain.Entities.CatalogItems
{
    public class CatalogItemValidator : AbstractValidator<CatalogItem>
    {
        public CatalogItemValidator()
        {
            RuleFor(foo => foo.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}