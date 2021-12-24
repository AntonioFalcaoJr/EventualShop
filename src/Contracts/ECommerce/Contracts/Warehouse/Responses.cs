using ECommerce.Abstractions.Queries.Responses;

namespace ECommerce.Contracts.Warehouse;

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