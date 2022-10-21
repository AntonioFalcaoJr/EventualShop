using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid Id, Guid ItemId, Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, decimal UnitPrice) : Message, ICommand;

    public record AddPaymentMethod(Guid Id, decimal Amount, Dto.IPaymentOption Option) : Message, ICommand;

    public record AddShippingAddress(Guid Id, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(Guid Id, Dto.Address Address) : Message, ICommand;

    public record CreateCart(Guid Id, Guid CustomerId) : Message, ICommand;

    public record CheckOutCart(Guid Id) : Message, ICommand;

    public record RemoveCartItem(Guid Id, Guid ItemId) : Message, ICommand;

    public record ChangeCartItemQuantity(Guid Id, Guid ItemId, ushort NewQuantity) : Message, ICommand;

    public record DiscardCart(Guid Id) : Message, ICommand;

    public record RemovePaymentMethod(Guid Id, Guid MethodId) : Message, ICommand;

    public record RebuildProjection(string Name, Guid Id = default) : Message, ICommand;
}