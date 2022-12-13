using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class IntegrationEvent
{
    public record ProjectionRebuilt(Guid Id, Guid AccountId, string FirstName, string LastName, 
        string Email, Dto.Address Address, bool IsDeleted) : Message, IEvent;
}