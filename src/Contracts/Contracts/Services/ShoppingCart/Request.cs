using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Request
{
    public record AddCartItem(Guid CartId, Guid CatalogId, Guid InventoryId, Dto.Product Product, int Quantity);

    public record AddCreditCard(Dto.CreditCard CreditCard, decimal Amount);

    public record AddDebitCard(Dto.DebitCard DebitCard, decimal Amount);

    public record AddPayPal(Dto.PayPal PayPal, decimal Amount);

    public record CreateCart(Guid CustomerId);
}