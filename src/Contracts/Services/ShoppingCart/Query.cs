using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Services.ShoppingCart;

public static class Query
{
    public record GetShoppingCartDetails(Guid CartId) : IQuery
    {
        public static implicit operator GetShoppingCartDetails(Protobuf.GetShoppingCartDetailsRequest request)
            => new(new Guid(request.CartId));
    }

    public record GetCustomerShoppingCartDetails(Guid CartId, Guid CustomerId) : IQuery
    {
        public static implicit operator GetCustomerShoppingCartDetails(Protobuf.GetCustomerShoppingCartDetailsRequest request)
            => new(new(request.CartId), new(request.CustomerId));
    }

    public record GetShoppingCartItemDetails(Guid CartId, Guid ItemId) : IQuery
    {
        public static implicit operator GetShoppingCartItemDetails(Protobuf.GetShoppingCartItemDetailsRequest request)
            => new(new(request.CartId), new(request.ItemId));
    }

    public record GetPaymentMethodDetails(Guid CartId, Guid MethodId) : IQuery
    {
        public static implicit operator GetPaymentMethodDetails(Protobuf.GetPaymentMethodDetailsRequest request)
            => new(new(request.CartId), new(request.MethodId));
    }

    public record ListShoppingCartItemsListItems(Guid CartId, Paging Paging) : IQuery
    {
        public static implicit operator ListShoppingCartItemsListItems(Protobuf.ListShoppingCartItemsListItemsRequest request)
            => new(new(request.CartId), request.Paging);
    }

    public record ListPaymentMethodsListItems(Guid CartId, Paging Paging) : IQuery
    {
        public static implicit operator ListPaymentMethodsListItems(Protobuf.ListPaymentMethodsListItemsRequest request)
            => new(new(request.CartId), request.Paging);
    }
}