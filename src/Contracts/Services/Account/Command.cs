using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Command
{
    public record CreateAccount(Guid Id, string FirstName, string LastName, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record AddShippingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommandWithId;

    public record AddBillingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommandWithId;

    public record DeleteAccount(Guid Id) : Message(CorrelationId: Id), ICommandWithId;

    public record DeleteShippingAddress(Guid Id, Guid AddressId) : Message(CorrelationId: Id), ICommandWithId;

    public record DeleteBillingAddress(Guid Id, Guid AddressId) : Message(CorrelationId: Id), ICommandWithId;

    public record PreferShippingAddress(Guid Id, Guid AddressId) : Message(CorrelationId: Id), ICommandWithId;

    public record PreferBillingAddress(Guid Id, Guid AddressId) : Message(CorrelationId: Id), ICommandWithId;
}