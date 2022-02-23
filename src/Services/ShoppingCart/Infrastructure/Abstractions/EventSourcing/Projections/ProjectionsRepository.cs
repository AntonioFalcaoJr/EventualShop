using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Paging;
using Infrastructure.Abstractions.EventSourcing.Projections.Contexts;
using Infrastructure.Abstractions.EventSourcing.Projections.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Abstractions.EventSourcing.Projections;

public abstract class ProjectionsRepository : IProjectionsRepository
{
    private readonly IMongoDbContext _context;

    protected ProjectionsRepository(IMongoDbContext context)
    {
        _context = context;
    }

    public Task<TProjection> GetAsync<TProjection, TId>(TId id, CancellationToken cancellationToken)
        where TProjection : IProjection
        => FindAsync<TProjection>(projection => projection.Id.Equals(id), cancellationToken);

    public Task<TProjection> FindAsync<TProjection>(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _context.GetCollection<TProjection>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);

    public Task<IPagedResult<TProjection>> GetAllAsync<TProjection>(IPaging paging, Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken)
        where TProjection : IProjection
    {
        var queryable = _context.GetCollection<TProjection>().AsQueryable().Where(predicate);
        return PagedResult<TProjection>.CreateAsync(paging, queryable, cancellationToken);
    }

    public Task UpsertAsync<TProjection>(TProjection replacement, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _context
            .GetCollection<TProjection>()
            .ReplaceOneAsync(
                filter: projection => projection.Id.Equals(replacement.Id),
                replacement: replacement,
                options: new ReplaceOptions { IsUpsert = true },
                cancellationToken: cancellationToken);

    public Task DeleteAsync<TProjection>(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken)
        where TProjection : IProjection
        => _context.GetCollection<TProjection>().DeleteOneAsync(filter, cancellationToken);
}