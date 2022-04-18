using System.Linq.Expressions;
using ECommerce.Abstractions.Projections;

namespace Application.Abstractions.EventSourcing.Projections;

public interface IProjectionsService
{
    Task ProjectAsync<TProjection>(TProjection projection, CancellationToken cancellationToken)
        where TProjection : IProjection;

    Task ProjectManyAsync<TProjection>(IEnumerable<TProjection> projections, CancellationToken cancellationToken)
        where TProjection : IProjection;

    Task UpdateFieldAsync<TProjection, TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken)
        where TProjection : IProjection
        where TId : struct;
}