using FluentValidation;

namespace Domain.Entities.CatalogBrands
{
    public class Validator : AbstractValidator<CatalogBrand>
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