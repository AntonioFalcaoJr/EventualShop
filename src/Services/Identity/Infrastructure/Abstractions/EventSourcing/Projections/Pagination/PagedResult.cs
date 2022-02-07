using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Abstractions.Messages.Queries.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Pagination;

public class PagedResult<T> : IPagedResult<T>
{
    private readonly IEnumerable<T> _items;
    private readonly int _limit;
    private readonly int _offset;

    private PagedResult(IEnumerable<T> items, int limit, int offset)
    {
        _items = items;
        _limit = limit;
        _offset = offset;
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

    public static async Task<IPagedResult<T>> CreateAsync(IPaging paging, IQueryable<T> source, CancellationToken cancellationToken)
    {
        paging ??= new Paging();
        var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
        return new PagedResult<T>(items, paging.Limit, paging.Offset);
    }

    private static IMongoQueryable<T> ApplyPagination(IPaging paging, IQueryable<T> source)
        => source.Skip(paging.Limit * paging.Offset).Take(paging.Limit + 1) as IMongoQueryable<T>;
}