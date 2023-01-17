using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using Infrastructure.Projections.Abstractions.Contexts;
using Infrastructure.Projections.Abstractions.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections;

public class ProjectionGateway<TProjection> : IProjectionGateway<TProjection>
    where TProjection : IProjection
{
    private readonly IMongoCollection<TProjection> _collection;

    public ProjectionGateway(IMongoDbContext context)
    {
        _collection = context.GetCollection<TProjection>();
    }

    public Task<TProjection?> GetAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct
        => FindAsync(projection => projection.Id.Equals(id), cancellationToken);

    public Task<TProjection?> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
        => _collection.AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken)!;

    public Task<IPagedResult<TProjection>?> ListAsync(Paging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
        => PagedResult<TProjection>.CreateAsync(paging, _collection.AsQueryable().Where(predicate), cancellationToken)!;

    public Task<IPagedResult<TProjection>?> ListAsync(Paging paging, CancellationToken cancellationToken)
        => PagedResult<TProjection>.CreateAsync(paging, _collection.AsQueryable(), cancellationToken)!;

    public Task UpsertAsync(TProjection replacement, CancellationToken cancellationToken)
        => _collection.ReplaceOneAsync(
            filter: projection => projection.Id == replacement.Id,
            replacement: replacement,
            options: new ReplaceOptions { IsUpsert = true },
            cancellationToken: cancellationToken);

    public Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        => _collection.DeleteManyAsync(filter, cancellationToken);

    public Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct
        => _collection.DeleteOneAsync(projection => projection.Id.Equals(id), cancellationToken);

    public Task UpdateFieldAsync<TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct
        => _collection.UpdateOneAsync(
            filter: projection => projection.Id.Equals(id),
            update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
            cancellationToken: cancellationToken);
}