using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;

namespace Application.UseCases.Queries;

public class ListShippingAddressesInteractor : IPagedInteractor<Query.ListShippingAddressesListItems, Projection.ShippingAddressListItem>
{
    private readonly IProjectionGateway<Projection.ShippingAddressListItem> _projectionGateway;

    public ListShippingAddressesInteractor(IProjectionGateway<Projection.ShippingAddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public ValueTask<IPagedResult<Projection.ShippingAddressListItem>> InteractAsync(Query.ListShippingAddressesListItems query, CancellationToken cancellationToken)
        => _projectionGateway.ListAsync(query.Paging, cancellationToken);
}