using Contracts.Services.ShoppingCart.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.ShoppingCarts.Validators;

namespace WebAPI.APIs.ShoppingCarts;

public static class Queries
{
    public record GetShoppingCartDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetShoppingCartDetailsRequest(GetShoppingCartDetails request)
            => new() { CartId = request.CartId.ToString() };
    }

    public record GetCustomerShoppingCartDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CustomerId, CancellationToken CancellationToken)
        : Validatable<GetCustomerShoppingCartDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetCustomerShoppingCartDetailsRequest(GetCustomerShoppingCartDetails request)
            => new() { CustomerId = request.CustomerId.ToString() };
    }

    public record GetShoppingCartItemDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartItemDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetShoppingCartItemDetailsRequest(GetShoppingCartItemDetails request)
            => new() { CartId = request.CartId.ToString(), ItemId = request.ItemId.ToString() };
    }

    public record GetPaymentMethodDetails(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<GetPaymentMethodDetailsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator GetPaymentMethodDetailsRequest(GetPaymentMethodDetails request)
            => new() { CartId = request.CartId.ToString(), MethodId = request.MethodId.ToString() };
    }

    public record ListPaymentMethodsListItems(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListPaymentMethodsListItemsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator ListPaymentMethodsListItemsRequest(ListPaymentMethodsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListShoppingCartItemsListItems(ShoppingCartService.ShoppingCartServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShoppingCartItemsListItemsValidator>, IQuery<ShoppingCartService.ShoppingCartServiceClient>
    {
        public static implicit operator ListShoppingCartItemsListItemsRequest(ListShoppingCartItemsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}