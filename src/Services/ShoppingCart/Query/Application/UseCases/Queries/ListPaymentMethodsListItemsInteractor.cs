using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class ListPaymentMethodsListItemsInteractor : IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem>
{
    private readonly IProjectionGateway<Projection.PaymentMethodListItem> _projectionGateway;

    public ListPaymentMethodsListItemsInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.PaymentMethodListItem>> InteractAsync(Query.ListPaymentMethodsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, method => method.CartId == query.CartId, cancellationToken);
}