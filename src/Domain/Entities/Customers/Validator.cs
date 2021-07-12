using FluentValidation;

namespace Domain.Entities.Customers
{
    public class Validator : AbstractValidator<Customer>
    {
        public Validator()
        {
            RuleFor(foo => foo.Age)
                .GreaterThanOrEqualTo(0);

            RuleFor(foo => foo.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}