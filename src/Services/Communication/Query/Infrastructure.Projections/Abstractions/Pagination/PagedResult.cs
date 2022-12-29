using Contracts.Abstractions.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Projections.Abstractions.Pagination;

public class PagedResult<T> : IPagedResult<T>
{
    private readonly List<T> _items;
    private readonly Paging _paging;

    public PagedResult(List<T> items, Paging paging)
    {
        _items = items;
        _paging = paging;
    }

    public IEnumerable<T> Items
        => _items.Take(_paging.Limit ?? 0);

    public Page Page
        => new()
        {
            Current = (_paging.Offset ?? 0) + 1,
            Size = Items.Count(),
            HasNext = _items.Count > _paging.Limit,
            HasPrevious = _paging.Offset > 0
        };

    public static async Task<IPagedResult<T>> CreateAsync(Paging paging, IQueryable<T> source, CancellationToken cancellationToken)
    {
        var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<T>(items, paging);
    }

    private static IMongoQueryable<T>? ApplyPagination(Paging paging, IQueryable<T> source)
        => source.Skip(paging.Limit * paging.Offset ?? 0).Take(paging.Limit ?? 0 + 1) as IMongoQueryable<T>;
}