using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections.Pagination;

public record PagedResult<TProjection>(IReadOnlyCollection<TProjection> Projections, Paging Paging)
    : IPagedResult<TProjection> where TProjection : IProjection
{
    public IReadOnlyCollection<TProjection> Items
        => Page.HasNext ? Projections.Take(Paging.Size).ToList() : Projections;

    public Page Page => new()
    {
        Number = Paging.Number,
        Size = Paging.Size,
        HasNext = Projections.Count > Paging.Size,
        HasPrevious = Paging.Number > 0
    };

    public static async ValueTask<IPagedResult<TProjection>> CreateAsync
        (Paging paging, IQueryable<TProjection> source, CancellationToken cancellationToken)
    {
        var projections = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<TProjection>(projections, paging);
    }

    private static IMongoQueryable<TProjection> ApplyPagination(Paging paging, IQueryable<TProjection> source)
        => (IMongoQueryable<TProjection>)source.Skip(paging.Size * (paging.Number - 1)).Take(paging.Size + 1);
}