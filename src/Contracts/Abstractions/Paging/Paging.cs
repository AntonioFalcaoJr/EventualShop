namespace Contracts.Abstractions.Paging;

public record struct Paging
{
    private const int UpperLimit = 100;
    private const int DefaultLimit = 10;

    private readonly int? _limit;
    public int? Offset { get; init; }

    public int? Limit
    {
        get => _limit;

        init => _limit = value switch
        {
            < 1 => DefaultLimit,
            > UpperLimit => UpperLimit,
            _ => value
        };
    }

    public static implicit operator Paging(Services.Catalog.Protobuf.Paging paging)
        => new() { Limit = paging.Limit, Offset = paging.Offset };

    public static implicit operator Services.Catalog.Protobuf.Paging(Paging paging)
        => new() { Limit = paging.Limit, Offset = paging.Offset };
}