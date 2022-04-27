using Contracts.Abstractions;

namespace Contracts.Services.Warehouse;

public static class Query
{
    public record GetInventoryItemDetails(Guid ProductId) : Message(CorrelationId: ProductId), IQuery;
}