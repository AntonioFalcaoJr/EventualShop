using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Request
{
    public record CreateCart(Guid CustomerId);

    public record AddShoppingCartItem(Dto.Product Product, int Quantity);

    public record AddCreditCard(Dto.CreditCard CreditCard);

    public record AddDebitCard(Dto.DebitCard DebitCard);

    public record AddPayPal(Dto.PayPal PayPal);

    public record AddAddress(Dto.Address Address);
}