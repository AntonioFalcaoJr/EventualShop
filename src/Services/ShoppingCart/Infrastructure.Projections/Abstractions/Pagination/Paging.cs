namespace Infrastructure.Projections.Abstractions.Pagination;

public record Paging 
{
    private const int DefaultLimit = 10;
    private readonly int _limit;

    public int Limit
    {
        get => _limit > 0 ? _limit : DefaultLimit;
        init => _limit = value;
    }

    public int Offset { get; init; }
}