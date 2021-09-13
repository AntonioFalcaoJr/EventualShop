using FluentValidation;

namespace Domain.Entities.CatalogItems
{
    public class Validator : AbstractValidator<CatalogItem>
    {
        public Validator()
        {
            RuleFor(foo => foo.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}