using FluentValidation;

namespace Domain.Entities.Catalogs
{
    public class Validator : AbstractValidator<Catalog>
    {
        public Validator()
        {
            RuleFor(catalog => catalog.Title)
                .NotNull()
                .NotEmpty();
        }
    }
}