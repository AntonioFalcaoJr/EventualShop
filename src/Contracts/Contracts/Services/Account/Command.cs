using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Command
{
    public record CreateAccount(Guid UserId, string Email) : Message, ICommand;

    public record DefineProfessionalAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record DefineResidenceAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record DeleteAccount(Guid AccountId) : Message, ICommand;

    public record UpdateProfile(Guid AccountId, DateOnly Birthdate, string Email, string FirstName, string LastName) : Message, ICommand;
}