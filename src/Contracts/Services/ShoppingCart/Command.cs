using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid CartId, Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, decimal UnitPrice) : Message, ICommand;

    public record AddPaymentMethod(Guid CartId, decimal Amount, Dto.IPaymentOption? Option) : Message, ICommand;

    public record AddShippingAddress(Guid CartId, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(Guid CartId, Dto.Address Address) : Message, ICommand;

    public record CreateCart(Guid CartId, Guid CustomerId) : Message, ICommand;

    public record CheckOutCart(Guid CartId) : Message, ICommand;

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Message, ICommand;

    public record ChangeCartItemQuantity(Guid CartId, Guid ItemId, ushort NewQuantity) : Message, ICommand;

    public record DiscardCart(Guid CartId) : Message, ICommand;

    public record RemovePaymentMethod(Guid CartId, Guid MethodId) : Message, ICommand;

    public record RebuildProjection(string Name) : Message, ICommand;
}