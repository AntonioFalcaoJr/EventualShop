using System.Linq.Expressions;
using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using Infrastructure.Projections.Abstractions.Contexts;
using Infrastructure.Projections.Abstractions.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections;

public class ProjectionRepository<TProjection> : IProjectionRepository<TProjection>
    where TProjection : IProjection
{
    private readonly IMongoCollection<TProjection> _collection;

    public ProjectionRepository(IMongoDbContext context)
        => _collection = context.GetCollection<TProjection>();

    public Task<TProjection> GetAsync<TId>(TId id, CancellationToken ct)
        where TId : struct
        => FindAsync(projection => projection.Id.Equals(id), ct);

    public Task<TProjection> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken ct)
        => _collection.AsQueryable().Where(predicate).FirstOrDefaultAsync(ct);

    public Task<IPagedResult<TProjection>> GetAllAsync(ushort limit, ushort offset, Expression<Func<TProjection, bool>> predicate, CancellationToken ct)
        => PagedResult<TProjection>.CreateAsync(limit, offset, _collection.AsQueryable().Where(predicate), ct);

    public Task<IPagedResult<TProjection>> GetAllAsync(ushort limit, ushort offset, CancellationToken ct)
        => PagedResult<TProjection>.CreateAsync(limit, offset, _collection.AsQueryable(), ct);

    public Task InsertAsync(TProjection projection, CancellationToken ct)
        => _collection.InsertOneAsync(projection, cancellationToken: ct);

    public Task UpsertAsync(TProjection replacement, CancellationToken ct)
        => _collection.ReplaceOneAsync(
            filter: projection => projection.Id == replacement.Id,
            replacement: replacement,
            options: new ReplaceOptions {IsUpsert = true},
            cancellationToken: ct);

    public Task UpsertManyAsync(IEnumerable<TProjection> replacements, CancellationToken ct)
    {
        var requests = replacements.Select(replacement => new ReplaceOneModel<TProjection>(
            filter: new ExpressionFilterDefinition<TProjection>(projection => projection.Id == replacement.Id),
            replacement: replacement) {IsUpsert = true});

        return _collection
            .WithWriteConcern(WriteConcern.Unacknowledged)
            .BulkWriteAsync(
                requests: requests,
                options: new BulkWriteOptions {IsOrdered = false},
                cancellationToken: ct);
    }

    public Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken ct)
        => _collection.DeleteManyAsync(filter, ct);

    public Task DeleteAsync<TId>(TId id, CancellationToken ct)
        => _collection.DeleteOneAsync(projection => projection.Id.Equals(id), ct);

    public Task UpdateFieldAsync<TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken ct)
        where TId : struct
        => _collection.UpdateOneAsync(
            filter: projection => projection.Id.Equals(id),
            update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
            cancellationToken: ct);
}