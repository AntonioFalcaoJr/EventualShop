namespace Domain.Abstractions;

public interface IIdentifier { }

public abstract record Identifier<TValue> : IIdentifier
    where TValue : notnull
{
    public static IIdentifier Undefined => typeof(TValue) switch
    {
        { } when typeof(TValue) == typeof(Guid) => new GuidIdentifier() as Identifier<TValue>,
        { } when typeof(TValue) == typeof(int) => new IntIdentifier() as Identifier<TValue>,
        { } when typeof(TValue) == typeof(long) => new LongIdentifier() as Identifier<TValue>,
        { } when typeof(TValue) == typeof(string) => new StringIdentifier() as Identifier<TValue>,
        _ => throw new ArgumentOutOfRangeException(nameof(TValue), "Undefined identifier not supported for this type.")
    } ?? throw new InvalidOperationException("Undefined identifier not supported for this type.");

    public TValue Value { get; }

    protected Identifier(TValue value)
        => Value = (TValue)Convert.ChangeType(value, typeof(TValue));

    protected Identifier(string value)
        => Value = (TValue)Convert.ChangeType(value, typeof(TValue));

    public static implicit operator string(Identifier<TValue> id)
        => id.Value.ToString() ?? throw new InvalidOperationException("Identifier cannot be null.");
}

public record StringIdentifier : Identifier<string>
{
    public StringIdentifier(string value = "Undefined")
        : base(value) { }
}

public record IntIdentifier : Identifier<int>
{
    public IntIdentifier(int value = 0)
        : base(value) { }
}

public record LongIdentifier : Identifier<long>
{
    public LongIdentifier(long value = 0)
        : base(value) { }
}

public record GuidIdentifier : Identifier<Guid>
{
    public GuidIdentifier(Guid value = new())
        : base(value) { }
}