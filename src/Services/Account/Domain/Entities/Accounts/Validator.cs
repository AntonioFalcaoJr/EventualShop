using FluentValidation;

namespace Domain.Entities.Accounts
{
    public class Validator : AbstractValidator<Account>
    {
        public Validator()
        {
            RuleFor(foo => foo.Age)
                .GreaterThan(0);

            RuleFor(foo => foo.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}