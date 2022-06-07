using Contracts.Abstractions.Messages;

namespace Contracts.Services.Warehouse;

public static class Query
{
    public record GetInventories(int Limit, int Offset) : Message, IQuery;

    public record GetInventoryItems(Guid InventoryId, int Limit, int Offset) : Message(CorrelationId: InventoryId), IQuery;
}