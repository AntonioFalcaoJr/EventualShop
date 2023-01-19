using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(Guid CartId, Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, string UnitPrice) : Message, ICommand;

    public record AddCreditCard(Guid CartId, string Amount, Dto.CreditCard CreditCard) : Message, ICommand;

    public record AddDebitCard(Guid CartId, string Amount, Dto.DebitCard DebitCard) : Message, ICommand;

    public record AddPayPal(Guid CartId, string Amount, Dto.PayPal PayPal) : Message, ICommand;

    public record AddShippingAddress(Guid CartId, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(Guid CartId, Dto.Address Address) : Message, ICommand;

    public record CreateCart(Guid CustomerId, string Currency) : Message, ICommand;

    public record CheckOutCart(Guid CartId) : Message, ICommand;

    public record RemoveCartItem(Guid CartId, Guid ItemId) : Message, ICommand;

    public record ChangeCartItemQuantity(Guid CartId, Guid ItemId, ushort NewQuantity) : Message, ICommand;

    public record DiscardCart(Guid CartId) : Message, ICommand;

    public record RemovePaymentMethod(Guid CartId, Guid MethodId) : Message, ICommand;

    public record RebuildProjection(string Name) : Message, ICommand;
}