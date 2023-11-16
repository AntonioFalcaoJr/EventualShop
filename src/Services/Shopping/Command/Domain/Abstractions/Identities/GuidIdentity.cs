using static Domain.Exceptions;

namespace Domain.Abstractions.Identities;

public interface IIdentifier;

public abstract record GuidIdentifier : IIdentifier
{
    public Guid Value { get; init; }
    
    protected GuidIdentifier()
    {
        Value = Guid.NewGuid();
    }

    protected GuidIdentifier(string value)
    {
        InvalidIdentifier.ThrowIf(
            Guid.TryParse(value, out var result) is false);

        Value = result;
    }

    public static implicit operator string(GuidIdentifier id) => id.Value.ToString();
    public static implicit operator Guid(GuidIdentifier id) => id.Value;
    public static bool operator ==(GuidIdentifier id, string value) => id.Value.CompareTo(value) is 0;
    public static bool operator !=(GuidIdentifier id, string value) => id.Value.CompareTo(value) is not 0;
    public override string ToString() => Value.ToString();
}