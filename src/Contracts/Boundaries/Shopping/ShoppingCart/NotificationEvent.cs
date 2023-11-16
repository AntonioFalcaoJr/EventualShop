using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Shopping.ShoppingCart;

public static class NotificationEvent
{
    public record CartProjectionRebuildRequested(string CartId, string Name) : Message, IEvent;
}