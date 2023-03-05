using Contracts.Abstractions.Messages;

namespace Contracts.Services.ShoppingCart;

public static class NotificationEvent
{
    public record CartProjectionRebuildRequested(Guid CartId, string Name) : Message, IEvent;
}