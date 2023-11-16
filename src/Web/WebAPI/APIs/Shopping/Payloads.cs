using Contracts.DataTransferObjects;

namespace WebAPI.APIs.Shopping;

public static class Payloads
{
    public record AddCartItemPayload(Guid CatalogId, Guid InventoryId, Dto.Product Product, ushort Quantity, Dto.Money UnitPrice);

    public record AddCreditCardPayload(Dto.Money Amount, Dto.CreditCard CreditCard);

    public record AddDebitCardPayload(Dto.Money Amount, Dto.DebitCard DebitCard);

    public record AddPaypalPayload(Dto.Money Amount, Dto.PayPal PayPal);
}