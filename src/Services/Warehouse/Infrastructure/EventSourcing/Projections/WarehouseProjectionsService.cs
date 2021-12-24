using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;

namespace Infrastructure.EventSourcing.Projections;

public class WarehouseProjectionsService : IWarehouseProjectionsService
{
    private readonly IWarehouseProjectionsRepository _repository;

    public WarehouseProjectionsService(IWarehouseProjectionsRepository repository)
    {
        _repository = repository;
    }

    public Task<InventoryItemDetailsProjection> GetProductDetailsAsync(Guid productId, CancellationToken cancellationToken)
        => _repository.FindAsync<InventoryItemDetailsProjection>(product => product.Id == productId, cancellationToken);

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.UpsertAsync(projection, cancellationToken);

    public Task RemoveAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _repository.DeleteAsync(filter, cancellationToken);
}