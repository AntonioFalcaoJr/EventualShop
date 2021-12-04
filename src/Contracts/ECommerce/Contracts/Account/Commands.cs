using System;
using ECommerce.Abstractions.Commands;

namespace ECommerce.Contracts.Account;

public static class Commands
{
    public record CreateAccount(Guid UserId, string Email, string FirstName) : Command;

    public record DefineProfessionalAddress(Guid AccountId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record DefineResidenceAddress(Guid AccountId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Command;

    public record DeleteAccount(Guid AccountId) : Command;

    public record UpdateProfile(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Command;
}