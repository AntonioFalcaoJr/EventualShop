using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Boundaries.Shopping.ShoppingCart;

public static class Query
{
    public record GetShoppingCartDetails(Guid CartId) : IQuery
    {
        public static implicit operator GetShoppingCartDetails(Contracts.Shopping.Queries.GetShoppingCartDetailsRequest request)
            => new(new Guid(request.CartId));
    }

    public record GetCustomerShoppingCartDetails(Guid CustomerId) : IQuery
    {
        public static implicit operator GetCustomerShoppingCartDetails(Contracts.Shopping.Queries.GetCustomerShoppingCartDetailsRequest request)
            => new(new Guid(request.CustomerId));
    }

    public record GetShoppingCartItemDetails(Guid CartId, Guid ItemId) : IQuery
    {
        public static implicit operator GetShoppingCartItemDetails(Contracts.Shopping.Queries.GetShoppingCartItemDetailsRequest request)
            => new(new(request.CartId), new(request.ItemId));
    }

    public record GetPaymentMethodDetails(Guid CartId, Guid MethodId) : IQuery
    {
        public static implicit operator GetPaymentMethodDetails(Contracts.Shopping.Queries.GetPaymentMethodDetailsRequest request)
            => new(new(request.CartId), new(request.MethodId));
    }

    public record ListShoppingCartItemsListItems(Guid CartId, Paging Paging) : IQuery
    {
        public static implicit operator ListShoppingCartItemsListItems(Contracts.Shopping.Queries.ListShoppingCartItemsListItemsRequest request)
            => new(new(request.CartId), request.Paging);
    }

    public record ListPaymentMethodsListItems(Guid CartId, Paging Paging) : IQuery
    {
        public static implicit operator ListPaymentMethodsListItems(Contracts.Shopping.Queries.ListPaymentMethodsListItemsRequest request)
            => new(new(request.CartId), request.Paging);
    }
}