using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Warehouses;

public static class Responses
{
    public record InventoryItemDetails : Response
    {
        public string Sku { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
    }
}