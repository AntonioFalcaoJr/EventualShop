using Contracts.DataTransferObjects;

namespace Contracts.Services.ShoppingCart;

public static class Request
{
    public record AddShoppingCartItem(Dto.Product Product, int Quantity);
}