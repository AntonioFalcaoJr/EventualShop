using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.Checkout;

public static class NotificationEvent
{
    public record CartProjectionRebuildRequested(Guid CartId, string Name) : Message, IEvent;
}