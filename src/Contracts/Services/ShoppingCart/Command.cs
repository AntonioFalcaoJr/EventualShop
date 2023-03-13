using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Command
{
    public record AddCartItem(string CartId, Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, Dto.Money UnitPrice) : Message, ICommand;

    public record AddCreditCard(string CartId, Dto.Money Amount, Dto.CreditCard CreditCard) : Message, ICommand;

    public record AddDebitCard(string CartId, Dto.Money Amount, Dto.DebitCard DebitCard) : Message, ICommand;

    public record AddPayPal(string CartId, Dto.Money Amount, Dto.PayPal PayPal) : Message, ICommand;

    public record AddShippingAddress(string CartId, Dto.Address Address) : Message, ICommand;

    public record AddBillingAddress(string CartId, Dto.Address Address) : Message, ICommand;

    public record CreateCart(string CustomerId, string Currency) : Message, ICommand;

    public record CheckOutCart(string CartId) : Message, ICommand;

    public record RemoveCartItem(string CartId, string ItemId) : Message, ICommand;

    public record ChangeCartItemQuantity(string CartId, string ItemId, ushort NewQuantity) : Message, ICommand;

    public record DiscardCart(string CartId) : Message, ICommand;

    public record RemovePaymentMethod(string CartId, Guid MethodId) : Message, ICommand;

    public record RebuildCartProjection(string Projection) : Message, ICommand;
}