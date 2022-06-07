using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid CartId, Guid CatalogId, Guid InventoryId, Dto.Product Product, int Quantity, string Sku, decimal UnitPrice) : Message(CorrelationId: CartId), ICommand;

    public record AddPaymentMethod(Guid CartId, decimal Amount, Dto.IPaymentOption Option) : Message(CorrelationId: CartId), ICommand;

    public record AddShippingAddress(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record ChangeBillingAddress(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record CreateCart(Guid CustomerId) : Message(CorrelationId: CustomerId), ICommand;

    public record CheckOutCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;

    public record ConfirmCartItem(Guid CartId, string Sku, int Quantity, DateTimeOffset Expiration) : Message(CorrelationId: CartId), ICommand;

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record IncreaseCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DecreaseCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DiscardCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;
}