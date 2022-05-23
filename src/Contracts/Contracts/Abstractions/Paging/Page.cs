namespace Contracts.Abstractions.Paging;

public record Page
{
    public int Current { get; init; }
    public int Size { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
}