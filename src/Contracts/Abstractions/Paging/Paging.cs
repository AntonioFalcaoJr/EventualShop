namespace Contracts.Abstractions.Paging;

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

    public static implicit operator Paging(Protobuf.Paging paging)
        => new(paging.Limit, paging.Offset);

    public static implicit operator Protobuf.Paging(Paging paging)
        => new() { Limit = paging.Limit, Offset = paging.Offset };
}