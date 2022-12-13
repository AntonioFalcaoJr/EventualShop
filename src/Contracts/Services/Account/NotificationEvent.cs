using Contracts.Abstractions.Messages;

namespace Contracts.Services.Account;

public static class NotificationEvent
{
    public record RebuildProjectionRequested(Guid AccountId, string Name) : Message, IEvent;
}
