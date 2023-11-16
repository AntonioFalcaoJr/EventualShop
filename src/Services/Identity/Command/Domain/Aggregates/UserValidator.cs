using Domain.ValueObjects.Emails;
using FluentValidation;

namespace Domain.Aggregates;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty();

        RuleFor(user => user.FirstName)
            .Length(4, 30);

        RuleFor(user => user.LastName)
            .Length(4, 30)
            .NotEqual(user => user.FirstName);

        RuleForEach(user => user.Emails)
            .NotEmpty()
            .SetValidator(new EmailValidator());

        RuleFor(user => user.Password)
            .MinimumLength(8)
            .MaximumLength(16)
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric");
    }
}