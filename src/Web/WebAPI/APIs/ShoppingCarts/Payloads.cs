using Contracts.DataTransferObjects;

namespace WebAPI.APIs.ShoppingCarts;

public static class Payloads
{
    public record struct AddCartItemPayload(Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, string UnitPrice);
}