using Contracts.DataTransferObjects;
using Contracts.Enumerations;
using Domain.Abstractions.Entities;

namespace Domain.Entities.Profiles;

public class Profile : Entity<Guid, ProfileValidator>
{
    private Profile(string firstName, string lastName, string email, DateOnly? birthdate, Gender gender)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Birthdate = birthdate;
        Gender = gender;
    }

    public Profile(string firstName, string lastName, string email)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public DateOnly? Birthdate { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }

    public void ChangeFirstName(string firstName)
        => FirstName = firstName;

    public void ChangeLastName(string lastName)
        => LastName = lastName;

    public void ChangeBirthdate(DateOnly birthdate)
        => Birthdate = birthdate;

    public void ChangeEmail(string email)
        => Email = email;

    public void ChangeGender(Gender gender)
        => Gender = gender;

    public static implicit operator Profile(Dto.Profile profile)
        => new(profile.FirstName, profile.LastName, profile.Email, profile.Birthdate, profile.Gender);

    public static implicit operator Dto.Profile(Profile profile)
        => new(profile.FirstName, profile.LastName, profile.Email, profile.Birthdate, profile.Gender);
}