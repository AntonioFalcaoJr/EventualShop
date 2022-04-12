using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Warehouses;

public static class Queries
{
    public record GetInventoryItemDetails(Guid ProductId) : Query(CorrelationId: ProductId);
}