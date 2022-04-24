using ECommerce.Abstractions;

namespace ECommerce.Contracts.Warehouses;

public static class Query
{
    public record GetInventoryItemDetails(Guid ProductId) : Message(CorrelationId: ProductId), IQuery;
}