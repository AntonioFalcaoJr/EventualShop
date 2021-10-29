using System;
using Domain.ValueObjects.Addresses;
using FluentValidation;

namespace Domain.ValueObjects.Profiles;

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

        When(profile => profile.ResidenceAddress is not null, () =>
        {
            RuleFor(profile => profile.ResidenceAddress)
                .NotEqual(profile => profile.ProfessionalAddress)
                .SetValidator(new AddressValidator());
        });

        When(profile => profile.ProfessionalAddress is not null, () =>
        {
            RuleFor(profile => profile.ProfessionalAddress)
                .NotEqual(profile => profile.ResidenceAddress)
                .SetValidator(new AddressValidator());
        });
    }
}