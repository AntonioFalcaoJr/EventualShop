using System.Linq.Expressions;
using Application.Abstractions;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using Infrastructure.Projections.Abstractions;
using Infrastructure.Projections.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Serilog;

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

    public async ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken)
    {
        try
        {
            await _collection.ReplaceOneAsync(
                filter: projection => projection.Id == replacement.Id && projection.Version < replacement.Version,
                replacement: replacement,
                options: new ReplaceOptions { IsUpsert = true },
                cancellationToken: cancellationToken);
        }
        catch (MongoWriteException e) when (e.WriteError.Category is ServerErrorCategory.DuplicateKey)
        {
            Log.Warning(
                "By passing Duplicate Key when inserting {ProjectionType} with Id {Id}",
                typeof(TProjection).Name, replacement.Id);
        }
    }

    public Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        => _collection.DeleteManyAsync(filter, cancellationToken);

    public Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken) where TId : struct
        => _collection.DeleteOneAsync(projection => projection.Id.Equals(id), cancellationToken);

    public Task UpdateFieldAsync<TField, TId>(TId id, long version, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken) where TId : struct
        => _collection.UpdateOneAsync(
            filter: projection => projection.Id.Equals(id) && projection.Version < version,
            update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
            cancellationToken: cancellationToken);
}