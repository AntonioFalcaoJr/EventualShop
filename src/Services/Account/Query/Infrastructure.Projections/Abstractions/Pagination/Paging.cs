namespace Infrastructure.Projections.Abstractions.Pagination;

public record struct Paging
{
    private const ushort UpperLimit = 100;
    private const ushort DefaultLimit = 10;

    private readonly ushort _limit;

    public ushort Limit
    {
        get => _limit;

        init => _limit = value switch
        {
            < 1 => DefaultLimit,
            > UpperLimit => UpperLimit,
            _ => value
        };
    }

    public ushort Offset { get; init; }
}