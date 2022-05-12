using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Request
{
    public record CreateInventory(Guid OwnerId);

    public record ReceiveInventoryItem(Dto.Product Product, int Quantity);

    public record IncreaseInventoryAdjust(int Quantity, string Reason);

    public record DecreaseInventoryAdjust(int Quantity, string Reason);
}