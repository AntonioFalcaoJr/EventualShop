using Contracts.Shopping.Queries;
using WebAPI.Abstractions;
using WebAPI.APIs.Shopping.Validators;
using static Contracts.Shopping.Queries.ShoppingQueryService;

namespace WebAPI.APIs.Shopping;

public static class Queries
{
    public record GetShoppingCartDetails(ShoppingQueryServiceClient Client, Guid CartId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartDetailsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator GetShoppingCartDetailsRequest(GetShoppingCartDetails request)
            => new() { CartId = request.CartId.ToString() };
    }

    public record GetCustomerShoppingCartDetails(ShoppingQueryServiceClient Client, Guid CustomerId, CancellationToken CancellationToken)
        : Validatable<GetCustomerShoppingCartDetailsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator GetCustomerShoppingCartDetailsRequest(GetCustomerShoppingCartDetails request)
            => new() { CustomerId = request.CustomerId.ToString() };
    }

    public record GetShoppingCartItemDetails(ShoppingQueryServiceClient Client, Guid CartId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<GetShoppingCartItemDetailsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator GetShoppingCartItemDetailsRequest(GetShoppingCartItemDetails request)
            => new() { CartId = request.CartId.ToString(), ItemId = request.ItemId.ToString() };
    }

    public record GetPaymentMethodDetails(ShoppingQueryServiceClient Client, Guid CartId, Guid MethodId, CancellationToken CancellationToken)
        : Validatable<GetPaymentMethodDetailsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator GetPaymentMethodDetailsRequest(GetPaymentMethodDetails request)
            => new() { CartId = request.CartId.ToString(), MethodId = request.MethodId.ToString() };
    }

    public record ListPaymentMethodsListItems(ShoppingQueryServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListPaymentMethodsListItemsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator ListPaymentMethodsListItemsRequest(ListPaymentMethodsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListShoppingCartItemsListItems(ShoppingQueryServiceClient Client, Guid CartId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListShoppingCartItemsListItemsValidator>, IQuery<ShoppingQueryServiceClient>
    {
        public static implicit operator ListShoppingCartItemsListItemsRequest(ListShoppingCartItemsListItems request)
            => new() { CartId = request.CartId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }
}