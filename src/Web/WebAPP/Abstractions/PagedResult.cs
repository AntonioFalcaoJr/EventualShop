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
    public ushort Number { get; init; } = 1;
    public ushort Size { get; init; } = 10;
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
    public ushort Previous => (ushort)(Number - 1);
    public ushort Next => (ushort)(Number + 1);
}

public record Paging
{
    private const ushort UpperSize = 100;
    private const ushort DefaultSize = 10;
    private const ushort DefaultNumber = 1;
    private const ushort Zero = 0;

    public Paging(ushort size = DefaultSize, ushort number = DefaultNumber)
    {
        Size = size switch
        {
            Zero => DefaultSize,
            > UpperSize => UpperSize,
            _ => size
        };

        Number = number is Zero
            ? DefaultNumber
            : number;
    }

    public ushort Size { get; }
    public ushort Number { get; }
}