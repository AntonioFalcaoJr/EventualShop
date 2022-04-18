using System.Linq.Expressions;
using Application.Abstractions.EventSourcing.Projections;
using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Projections;

namespace Infrastructure.Projections.Abstractions;

public abstract class ProjectionsService : IProjectionsService
{
    protected readonly ICatalogProjectionsRepository Repository;

    protected ProjectionsService(ICatalogProjectionsRepository repository)
    {
        Repository = repository;
    }

    public Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection
        => Repository.UpsertAsync(projection, cancellationToken);

    public Task ProjectManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken)
        where TProjection : IProjection
        => Repository.UpsertManyAsync(projections, cancellationToken);

    public Task UpdateFieldAsync<TProjection, TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken)
        where TProjection : IProjection
        where TId : struct
        => Repository.UpdateFieldAsync(id, field, value, cancellationToken);
}