using Contracts.Enumerations;
using FluentValidation;

namespace Contracts.DataTransferObjects.Validators;

public class ProfileValidator : AbstractValidator<Dto.Profile>
{
    public ProfileValidator()
    {
        RuleFor(profile => profile.Email)
            .EmailAddress();

        RuleFor(profile => profile.FirstName)
            .Length(5, 50)
            .NotEqual(profile => profile.LastName, StringComparer.OrdinalIgnoreCase);

        RuleFor(profile => profile.LastName)
            .Length(5, 50)
            .MaximumLength(50);

        RuleFor(profile => profile.Birthdate)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTimeOffset.Now.AddYears(-18).DateTime));

        RuleFor(profile => profile.Gender)
            .Must(gender => Gender.TryFromName(gender, out _));
    }
}