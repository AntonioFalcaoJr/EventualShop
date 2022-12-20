using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class IntegrationEvent
{
    public record ProjectionRebuilt(Guid AccountId, Dto.Account Account) : Message, IEvent;
}