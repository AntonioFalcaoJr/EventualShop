using Domain.Entities.CatalogItems;
using FluentValidation;

namespace Domain.Aggregates
{
    public class CatalogValidator : AbstractValidator<Catalog>
    {
        public CatalogValidator()
        {
            RuleFor(catalog => catalog.Title)
                .NotNull()
                .NotEmpty();

            RuleForEach(catalog => catalog.Items)
                .SetValidator(new CatalogItemValidator());
        }
    }
}