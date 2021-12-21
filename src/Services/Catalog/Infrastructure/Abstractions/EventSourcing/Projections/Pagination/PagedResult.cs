using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Pagination;

public class PagedResult<T> : IPagedResult<T>
{
    private readonly IEnumerable<T> _items;
    private readonly int _limit;
    private readonly int _offset;

    private PagedResult(IEnumerable<T> items, int offset, int limit)
    {
        _items = items;
        _offset = offset;
        _limit = limit;
    }

    public IEnumerable<T> Items
        => _items.Take(_limit);

    public IPageInfo PageInfo
        => new PageInfo
        {
            Current = _offset + 1,
            Size = Items.Count(),
            HasNext = _limit < _items.Count(),
            HasPrevious = _offset > 0
        };

    public static async Task<IPagedResult<T>> CreateAsync(Paging paging, IMongoQueryable<T> source, CancellationToken cancellationToken)
    {
        paging ??= new Paging();
        var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<T>(items, paging.Offset, paging.Limit);
    }

    private static IMongoQueryable<T> ApplyPagination(Paging paging, IMongoQueryable<T> source)
        => source.Skip(paging.Limit * paging.Offset).Take(paging.Limit + 1);
}