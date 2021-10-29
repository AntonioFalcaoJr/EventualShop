using Application.Abstractions.EventSourcing.Projections.Pagination;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Pagination;

public record PageInfo : IPageInfo
{
    public int Current { get; init; }
    public int Size { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
}