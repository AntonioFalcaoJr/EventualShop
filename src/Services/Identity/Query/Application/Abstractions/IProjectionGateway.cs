using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;

namespace Application.Abstractions;

public interface IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<TProjection?> GetAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    Task<IPagedResult<TProjection>?> ListAsync(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken);
    Task<IPagedResult<TProjection>?> ListAsync(Paging paging, CancellationToken cancellationToken);
    Task InsertAsync(TProjection projection, CancellationToken cancellationToken);
    Task UpsertAsync(TProjection replacement, CancellationToken cancellationToken);
    Task UpsertManyAsync(IEnumerable<TProjection> replacements, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken);
    Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct;
    Task UpdateFieldAsync<TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct;
}