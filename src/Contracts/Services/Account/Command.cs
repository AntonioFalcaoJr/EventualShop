using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Command
{
    public record CreateAccount(string FirstName, string LastName, string Email) : Message, ICommand;

    public record AddShippingAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(Guid AccountId, Dto.Address Address) : Message, ICommand;

    public record DeleteAccount(Guid AccountId) : Message, ICommand;

    public record DeleteShippingAddress(Guid AccountId, Guid AddressId) : Message, ICommand;

    public record DeleteBillingAddress(Guid AccountId, Guid AddressId) : Message, ICommand;

    public record PreferShippingAddress(Guid AccountId, Guid AddressId) : Message, ICommand;

    public record PreferBillingAddress(Guid AccountId, Guid AddressId) : Message, ICommand;

    public record RequestRebuildProjection(string Name, Guid Id = default) : Message, ICommand;
}