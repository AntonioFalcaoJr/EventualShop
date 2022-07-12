using Contracts.Abstractions.Messages;

namespace Contracts.Services.Warehouse;

public static class Query
{
    public record GetInventories(ushort Limit, ushort Offset) : Message, IQuery;

    public record GetInventoryItems(Guid InventoryId, ushort Limit, ushort Offset) : Message(CorrelationId: InventoryId), IQuery;
}