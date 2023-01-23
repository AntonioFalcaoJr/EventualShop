using Contracts.DataTransferObjects;

namespace WebAPI.APIs.ShoppingCarts;

public static class Payloads
{
    public record AddCartItemPayload(Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, string UnitPrice);

    public record AddCreditCardPayload(string Amount, Dto.CreditCard CreditCard);

    public record AddDebitCardPayload(string Amount, Dto.DebitCard DebitCard);

    public record AddPaypalPayload(string Amount, Dto.PayPal PayPal);

    public record CreateCartPayload(Guid CustomerId, string Currency);
}