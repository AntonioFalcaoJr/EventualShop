using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;

namespace Contracts.Services.ShoppingCart;

public static class Query
{
    public record GetShoppingCartDetails(Guid CartId) : Message, IQuery;

    public record GetCustomerShoppingCartDetails(Guid CustomerId) : Message, IQuery;

    public record GetShoppingCartItemDetails(Guid ItemId) : Message, IQuery;

    public record GetPaymentMethodDetails(Guid PaymentMethodId) : Message, IQuery;

    public record ListShoppingCartItemsListItems(Guid CartId, Paging Paging) : Message, IQuery;

    public record ListPaymentMethodsListItems(Guid CartId, Paging Paging) : Message, IQuery;
}