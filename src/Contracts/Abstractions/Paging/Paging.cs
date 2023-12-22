namespace Contracts.Abstractions.Paging;

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

    public static implicit operator Paging(Protobuf.Paging paging)
        => new((ushort) paging.Limit, (ushort)paging.Offset);

    public static implicit operator Protobuf.Paging(Paging paging)
        => new() { Limit = paging.Size, Offset = paging.Number };
}