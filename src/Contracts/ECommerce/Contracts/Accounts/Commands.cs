using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Accounts;

public static class Commands
{
    public record CreateAccount(Guid UserId, string Email, string FirstName) : Command;

    public record DefineProfessionalAddress(Guid AccountId, Models.Address Address) : Command;

    public record DefineResidenceAddress(Guid AccountId, Models.Address Address) : Command;

    public record DeleteAccount(Guid AccountId) : Command;

    public record UpdateProfile(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Command;
}