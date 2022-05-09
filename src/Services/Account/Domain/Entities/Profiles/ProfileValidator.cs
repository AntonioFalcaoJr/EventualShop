using FluentValidation;

namespace Domain.Entities.Profiles;

public class ProfileValidator : AbstractValidator<Profile>
{
    public ProfileValidator()
    {
        RuleFor(profile => profile.Birthdate)
            .NotEqual(default(DateOnly));

        RuleFor(profile => profile.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(profile => profile.FirstName)
            .NotNull()
            .NotEmpty()
            .NotEqual(profile => profile.LastName);

        When(profile => profile.LastName is not null, () =>
        {
            RuleFor(profile => profile.LastName)
                .NotEqual(profile => profile.FirstName);
        });
    }
}