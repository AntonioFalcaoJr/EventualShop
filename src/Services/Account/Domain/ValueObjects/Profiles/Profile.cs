using System;
using Domain.Abstractions.ValueObjects;
using Domain.ValueObjects.Addresses;

namespace Domain.ValueObjects.Profiles;

public record Profile : ValueObject
{
    public Profile(string email, string firstName)
    {
        SetEmail(email);
        SetFirstName(firstName);
    }

    public DateOnly? Birthdate { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Address ResidenceAddress { get; private set; }
    public Address ProfessionalAddress { get; private set; }

    public void SetFirstName(string firstName)
        => FirstName = firstName;

    public void SetLastName(string lastName)
        => LastName = lastName;

    public void SetBirthdate(DateOnly birthdate)
        => Birthdate = birthdate;

    public void SetEmail(string email)
        => Email = email;

    public void SetResidenceAddress(Address residenceAddress)
        => ResidenceAddress = residenceAddress;

    public void SetProfessionalAddress(Address professionalAddress)
        => ProfessionalAddress = professionalAddress;

    protected override bool Validate()
        => OnValidate<ProfileValidator, Profile>();
}