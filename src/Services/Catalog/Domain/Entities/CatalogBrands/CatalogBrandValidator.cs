using System;
using Domain.Abstractions.Validators;

namespace Domain.Entities.CatalogBrands
{
    public class Validator : EntityValidator<CatalogBrand, Guid>
    {
        public Validator()
        {
            // RuleFor(foo => foo.Age)
            //     .GreaterThan(0);
            //
            // RuleFor(foo => foo.Name)
            //     .NotNull()
            //     .NotEmpty();
        }
    }
}