using System.Text.Json.Serialization;
using Ardalis.SmartEnum;

namespace Domain.Enumerations;

public sealed class CartStatus : SmartEnum<CartStatus>
{
    [JsonConstructor]
    private CartStatus(string name, int value)
        : base(name, value) { }

    public static readonly CartStatus Abandoned = new(nameof(Abandoned), 1);
    public static readonly CartStatus Active = new(nameof(Active), 2);
    public static readonly CartStatus CheckedOut = new(nameof(CheckedOut), 3);

    public static implicit operator CartStatus(string name)
        => FromName(name);

    public static implicit operator CartStatus(int value)
        => FromValue(value);

    public static implicit operator string(CartStatus status)
        => status.Name;

    public static implicit operator int(CartStatus status)
        => status.Value;
}