using Application.Abstractions;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;

namespace Application.UseCases.Queries;

public class ListAddressesInteractor : IInteractor<Query.ListAddresses, IPagedResult<Projection.AddressListItem>>
{
    private readonly IProjectionGateway<Projection.AddressListItem> _projectionGateway;

    public ListAddressesInteractor(IProjectionGateway<Projection.AddressListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public Task<IPagedResult<Projection.AddressListItem>?> InteractAsync(Query.ListAddresses query, CancellationToken cancellationToken)
        => _projectionGateway.GetAllAsync(query.Limit, query.Offset, cancellationToken);
}