using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Queries;

public class ListPaymentMethodsListItemsInteractor : IInteractor<Query.ListPaymentMethodsListItems, IPagedResult<Projection.PaymentMethodListItem>>
{
    private readonly IProjectionGateway<Projection.PaymentMethodListItem> _projectionGateway;

    public ListPaymentMethodsListItemsInteractor(IProjectionGateway<Projection.PaymentMethodListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.PaymentMethodListItem>?> InteractAsync(Query.ListPaymentMethodsListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, method => method.CartId == query.CartId, cancellationToken);
}