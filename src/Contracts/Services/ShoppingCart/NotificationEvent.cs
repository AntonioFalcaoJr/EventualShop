using Contracts.Abstractions.Messages;

namespace Contracts.Services.ShoppingCart;

public static class NotificationEvent
{
    public record ProjectionRebuildRequested(Guid AggregateId, string Name) : Message, IEvent;
}