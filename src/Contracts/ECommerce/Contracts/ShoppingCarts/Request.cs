using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class Request
{
    public record CreateCart(Guid CustomerId);

    public record AddShoppingCartItem(Models.Product Product, int Quantity);

    public record AddCreditCard(Models.CreditCard CreditCard);

    public record AddDebitCard(Models.DebitCard DebitCard);

    public record AddPayPal(Models.PayPal PayPal);

    public record AddAddress(Models.Address Address);
}