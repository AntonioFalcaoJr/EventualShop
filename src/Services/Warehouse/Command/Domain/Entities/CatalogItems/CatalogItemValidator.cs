using FluentValidation;

namespace Domain.Entities.CatalogItems;

public class CatalogItemValidator : AbstractValidator<CatalogItem>
{
    public CatalogItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0);
    }
}