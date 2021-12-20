using Domain.Abstractions.Validators;
using Domain.Entities.CatalogItems;
using FluentValidation;

namespace Domain.Aggregates;

public class CatalogValidator : EntityValidator<Catalog, Guid>
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