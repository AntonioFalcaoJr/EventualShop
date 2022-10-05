using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid Id, Guid ItemId, Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, decimal UnitPrice) : Message(CorrelationId: Id), ICommand;

    public record AddPaymentMethod(Guid Id, decimal Amount, Dto.IPaymentOption Option) : Message(CorrelationId: Id), ICommand;

    public record AddShippingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommand;

    public record AddBillingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommand;

    public record CreateCart(Guid Id, Guid CustomerId) : Message(CorrelationId: Id), ICommand;

    public record CheckOutCart(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record RemoveCartItem(Guid Id, Guid ItemId) : Message(CorrelationId: Id), ICommand;

    public record ChangeCartItemQuantity(Guid Id, Guid ItemId, ushort Quantity) : Message(CorrelationId: Id), ICommand;

    public record DiscardCart(Guid Id) : Message(CorrelationId: Id), ICommand;
}