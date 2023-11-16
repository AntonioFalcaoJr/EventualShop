using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections.Pagination;

public record PagedResult<TProjection>(IReadOnlyCollection<TProjection> Projections, Paging Paging) : IPagedResult<TProjection>
    where TProjection : IProjection
{
    public IReadOnlyCollection<TProjection> Items
        => Page.HasNext ? Projections.Take(Paging.Limit).ToList() : Projections;

    public Page Page => new()
    {
        Current = Paging.Offset + 1,
        Size = Paging.Limit,
        HasNext = Paging.Limit < Projections.Count,
        HasPrevious = Paging.Offset > 0
    };

    public static async ValueTask<IPagedResult<TProjection>> CreateAsync(Paging paging, IQueryable<TProjection> source, CancellationToken cancellationToken)
    {
        var projections = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<TProjection>(projections, paging);
    }

    private static IMongoQueryable<TProjection> ApplyPagination(Paging paging, IQueryable<TProjection> source)
        => (IMongoQueryable<TProjection>)source.Skip(paging.Limit * paging.Offset).Take(paging.Limit + 1);
}