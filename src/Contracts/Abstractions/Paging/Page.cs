namespace Contracts.Abstractions.Paging;

public record Page
{
    public int Current { get; init; }
    public int Size { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }

    public static implicit operator Abstractions.Protobuf.Page(Page page)
        => new()
        {
            Current = page.Current,
            Size = page.Size,
            HasPrevious = page.HasPrevious,
            HasNext = page.HasNext
        };

    public static implicit operator Page(Abstractions.Protobuf.Page page)
        => new()
        {
            Current = page.Current,
            Size = page.Size,
            HasPrevious = page.HasPrevious,
            HasNext = page.HasNext
        };
}