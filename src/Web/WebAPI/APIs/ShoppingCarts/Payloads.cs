using Contracts.DataTransferObjects;

namespace WebAPI.APIs.ShoppingCarts;

public static class Payloads
{
    public record struct AddCartItemPayload(Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, string UnitPrice);

    public record struct AddCreditCardPayload(string Amount, Dto.CreditCard CreditCard);

    public record struct AddDebitCardPayload(string Amount, Dto.DebitCard DebitCard);

    public record struct AddPaypalPayload(string Amount, Dto.PayPal PayPal);

    public record struct CreateCartPayload(Guid CustomerId, string Currency);
}