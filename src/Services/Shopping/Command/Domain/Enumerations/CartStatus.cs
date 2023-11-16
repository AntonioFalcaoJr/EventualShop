namespace Domain.Enumerations;

public record CartStatus(string Name, int Value)
{
    public static readonly CartStatusEmpty Empty = new();
    public static readonly CartStatusOpen Open = new();
    public static readonly CartStatusAbandoned Abandoned = new();
    public static readonly CartStatusCheckedOut CheckedOut = new();

    public static explicit operator CartStatus(string name)
        => typeof(CartStatus).GetField(name)?.GetValue(default) as CartStatus
           ?? throw new ArgumentException($"Invalid {nameof(CartStatus)} name: {name}");

    public static explicit operator CartStatus(int value)
        => typeof(CartStatus).GetFields()
               .Select(field => field.GetValue(default) as CartStatus)
               .FirstOrDefault(status => status?.Value == value)
           ?? throw new ArgumentException($"Invalid {nameof(CartStatus)} value: {value}");

    public static implicit operator string(CartStatus status) => status.Name;
    public static implicit operator int(CartStatus status) => status.Value;
    public override string ToString() => Name;
}

public record CartStatusEmpty() : CartStatus(nameof(Empty), 0);

public record CartStatusOpen() : CartStatus(nameof(Open), 1);

public record CartStatusAbandoned() : CartStatus(nameof(Abandoned), 2);

public record CartStatusCheckedOut() : CartStatus(nameof(CheckedOut), 3);