using Domain.Abstractions.Entities;

namespace Domain.Entities.CatalogBrands;

public class CatalogBrand : Entity<Guid>
{
    protected override bool Validate()
        => OnValidate<Validator, CatalogBrand>();
}