using FluentValidation;

namespace Domain.Entities.Profiles;

public class ProfileValidator : AbstractValidator<Profile>
{
    public ProfileValidator()
    {
        RuleFor(profile => profile.Email)
            .EmailAddress();

        RuleFor(profile => profile.FirstName)
            .NotNull()
            .NotEmpty()
            .NotEqual(profile => profile.LastName);

        When(profile => profile.Birthdate is not null, () =>
        {
            RuleFor(profile => profile.Birthdate)
                .LessThan(DateOnly.FromDateTime(DateTimeOffset.Now.AddYears(-18).DateTime));
        });
        
        When(profile => profile.LastName is not null, () =>
        {
            RuleFor(profile => profile.LastName)
                .NotEmpty()
                .NotEqual(profile => profile.FirstName);
        });
    }
}