using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Warehouses;

public static class Query
{
    public record GetInventoryItemDetails(Guid ProductId) : Message(CorrelationId: ProductId), IQuery;
}