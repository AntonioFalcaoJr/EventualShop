using System;
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.Warehouse;

public static class Queries
{
    public record GetInventoryItemDetails(Guid ProductId) : Query(CorrelationId: ProductId);
}