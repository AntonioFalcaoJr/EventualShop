using System.Linq.Expressions;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;

namespace Application.Abstractions;

public interface IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<TProjection?> GetAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    ValueTask<IPagedResult<TProjection>> ListAsync(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    ValueTask<IPagedResult<TProjection>> ListAsync(Paging paging, CancellationToken cancellationToken);
    ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken);
    ValueTask RebuildInsertAsync(TProjection replacement, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken);
    Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    Task UpdateFieldAsync<TField, TId>(TId id, long version, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct;
}