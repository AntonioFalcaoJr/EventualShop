using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid CartId, Dto.Product Product, int Quantity) : Message(CorrelationId: CartId), ICommand;

    public record AddCreditCard(Guid CartId, Dto.CreditCard CreditCard) : Message(CorrelationId: CartId), ICommand;

    public record AddDebitCard(Guid CartId, Dto.DebitCard DebitCard) : Message(CorrelationId: CartId), ICommand;

    public record AddPayPal(Guid CartId, Dto.PayPal PayPal) : Message(CorrelationId: CartId), ICommand;

    public record AddShippingAddress(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record ChangeBillingAddress(Guid CartId, Dto.Address Address) : Message(CorrelationId: CartId), ICommand;

    public record CreateCart(Guid CustomerId) : Message(CorrelationId: CustomerId), ICommand;

    public record CheckOutCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record IncreaseCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DecreaseCartItem(Guid CartId, Guid ItemId) : Message(CorrelationId: CartId), ICommand;

    public record DiscardCart(Guid CartId) : Message(CorrelationId: CartId), ICommand;
}