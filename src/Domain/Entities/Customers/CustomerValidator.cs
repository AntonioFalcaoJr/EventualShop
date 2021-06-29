using FluentValidation;

namespace Domain.Entities.Customers
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(foo => foo.Age)
                .GreaterThanOrEqualTo(0);

            RuleFor(foo => foo.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}