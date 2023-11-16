using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Queries;

public class ListShippingAddressesInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    : IPagedInteractor<Query.ListShippingAddressesListItems, Projection.ShippingAddressListItem>
{
    public ValueTask<IPagedResult<Projection.ShippingAddressListItem>> InteractAsync(Query.ListShippingAddressesListItems query, CancellationToken cancellationToken)
        => projectionGateway.ListAsync(query.Paging, cancellationToken);
}