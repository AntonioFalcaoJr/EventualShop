namespace WebAPP.Abstractions;

public record PagedResult<T>(IEnumerable<T> Items, Page Page) : IPagedResult<T>
{
    public static IPagedResult<T> Empty => new PagedResult<T>(Enumerable.Empty<T>(), new());
}

public interface IPagedResult<out TProjection>
{
    IEnumerable<TProjection> Items { get; }
    Page Page { get; }
}

public record Page
{
    public int Current { get; init; } = 1;
    public int Size { get; init; } = 10;
    public bool HasPrevious { get; init; }  
    public bool HasNext { get; init; }
}

public record Paging
{
    private const int UpperLimit = 100;
    private const int DefaultLimit = 10;
    private const int DefaultOffset = 0;

    public Paging(int? limit, int? offset)
    {
        Limit = limit switch
        {
            null or < 1 => DefaultLimit,
            > UpperLimit => UpperLimit,
            _ => limit.Value
        };

        Offset = offset ?? DefaultOffset;
    }

    public int Offset { get; }
    public int Limit { get; }
}