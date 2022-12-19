using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class IntegrationEvent
{
    public record ProjectionRebuilt(Guid AccountId, Dto.Profile Profile, Dictionary<Guid, Dto.Address> Addresses, 
        bool WishToReceiveNews, bool AcceptedPolicies, bool IsDeleted) : Message, IEvent;
}