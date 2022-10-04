using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid Id, Guid CatalogId, Guid InventoryId, Dto.Product Product, int Quantity, string Sku, decimal UnitPrice) : Message(CorrelationId: Id), ICommand;

    public record AddPaymentMethod(Guid Id, decimal Amount, Dto.IPaymentOption Option) : Message(CorrelationId: Id), ICommand;

    public record AddShippingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommand;

    public record AddBillingAddress(Guid Id, Dto.Address Address) : Message(CorrelationId: Id), ICommand;

    public record CreateCart(Guid Id, Guid CustomerId) : Message(CorrelationId: Id), ICommand;

    public record CheckOutCart(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record ConfirmCartItem(Guid Id, string Sku, int Quantity, DateTimeOffset Expiration) : Message(CorrelationId: Id), ICommand;

    public record RemoveCartItem(Guid Id, Guid ItemId) : Message(CorrelationId: Id), ICommand;

    public record IncreaseCartItem(Guid Id, Guid ItemId) : Message(CorrelationId: Id), ICommand;

    public record DecreaseCartItem(Guid Id, Guid ItemId) : Message(CorrelationId: Id), ICommand;

    public record DiscardCart(Guid Id) : Message(CorrelationId: Id), ICommand;
}