using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Accounts;

public static class Command
{
    public record CreateAccount(Guid UserId, string Email) : Message, ICommand;

    public record DefineProfessionalAddress(Guid AccountId, Models.Address Address) : Message, ICommand;

    public record DefineResidenceAddress(Guid AccountId, Models.Address Address) : Message, ICommand;

    public record DeleteAccount(Guid AccountId) : Message, ICommand;

    public record UpdateProfile(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Message, ICommand;
}