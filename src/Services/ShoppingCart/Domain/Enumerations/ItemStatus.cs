using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace Domain.Enumerations;

public class ItemStatus : SmartEnum<ItemStatus>
{
    [JsonConstructor]
    private ItemStatus(string name, int value)
        : base(name, value) { }

    public static readonly ItemStatus Confirmed = new(nameof(Confirmed), 1);
    public static readonly ItemStatus Unconfirmed = new(nameof(Unconfirmed), 2);

    public static implicit operator ItemStatus(string name)
        => FromName(name);

    public static implicit operator ItemStatus(int value)
        => FromValue(value);

    public static implicit operator string(ItemStatus status)
        => status.Name;

    public static implicit operator int(ItemStatus status)
        => status.Value;
}