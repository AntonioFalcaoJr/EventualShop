using FluentValidation;

namespace Domain.ValueObjects.Emails;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(email => email.Address)
            .NotEmpty()
            .EmailAddress();

        RuleFor(email => email.Status)
            .NotNull();
    }
}