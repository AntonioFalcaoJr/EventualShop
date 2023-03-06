using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections.Pagination;

public class PagedResult<TProjection> : IPagedResult<TProjection>
    where TProjection : IProjection
{
    private readonly IReadOnlyList<TProjection> _items;
    private readonly Paging _paging;

    public PagedResult(IReadOnlyList<TProjection> items, Paging paging)
    {
        _items = items;
        _paging = paging;
    }

    public IReadOnlyList<TProjection> Items
        => _items.Take(_paging.Limit).ToList().AsReadOnly();

    public Page Page
        => new()
        {
            Current = _paging.Offset + 1,
            Size = Items.Count,
            HasNext = _items.Count > _paging.Limit,
            HasPrevious = _paging.Offset > 0
        };

    public static async ValueTask<IPagedResult<TProjection>> CreateAsync(Paging paging, IQueryable<TProjection> source, CancellationToken cancellationToken)
    {
        var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<TProjection>(items, paging);
    }

    private static IMongoQueryable<TProjection> ApplyPagination(Paging paging, IQueryable<TProjection> source)
        => (IMongoQueryable<TProjection>)source.Skip(paging.Limit * paging.Offset).Take(paging.Limit);
}