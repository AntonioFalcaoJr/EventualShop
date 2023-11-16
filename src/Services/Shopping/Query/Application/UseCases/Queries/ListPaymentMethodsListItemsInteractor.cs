using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Shopping.ShoppingCart;

namespace Application.UseCases.Queries;

public class ListPaymentMethodsListItemsInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    : IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem>
{
    public ValueTask<IPagedResult<Projection.PaymentMethodListItem>> InteractAsync(Query.ListPaymentMethodsListItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, method => method.CartId == query.CartId, cancellationToken);
}