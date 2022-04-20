using System.Linq.Expressions;
using ECommerce.Abstractions.Messages.Queries.Paging;
using ECommerce.Abstractions.Projections;

namespace Application.Abstractions.EventSourcing.Projections;

public interface IProjectionsRepository<TProjection>
    where TProjection : IProjection
{
    Task<TProjection> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<TProjection> GetAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    Task<IPagedResult<TProjection>> GetAsync(int limit, int offset, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<IPagedResult<TProjection>> GetAsync(int limit, int offset, CancellationToken cancellationToken);
    Task InsertAsync(TProjection projection, CancellationToken cancellationToken);
    Task UpsertAsync(TProjection replacement, CancellationToken cancellationToken);
    Task UpsertManyAsync(IEnumerable<TProjection> replacements, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken);
    Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken);
    Task UpdateFieldAsync<TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct;
}