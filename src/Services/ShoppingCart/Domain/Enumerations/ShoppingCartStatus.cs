using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public sealed class ShoppingCartStatus : SmartEnum<ShoppingCartStatus>
{
    [JsonConstructor]
    private ShoppingCartStatus(string name, int value)
        : base(name, value) { }

    public static readonly ShoppingCartStatus Abandoned = new(nameof(Abandoned), 1);
    public static readonly ShoppingCartStatus CheckedOut = new(nameof(CheckedOut), 2);
    public static readonly ShoppingCartStatus Confirmed = new(nameof(Confirmed), 3);
}