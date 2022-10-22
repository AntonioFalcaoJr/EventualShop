using Ardalis.SmartEnum;

namespace Contracts.Enumerations;

// TODO - Move it to Account Service project
public class Gender : SmartEnum<Gender>
{
    public Gender(string name, int value)
        : base(name, value) { }

    public static readonly Gender Male = new(nameof(Male), 1);
    public static readonly Gender Female = new(nameof(Female), 2);
    public static readonly Gender NonBinary = new(nameof(NonBinary), 3);
    public static readonly Gender Undefined = new(nameof(Undefined), 4);

    public static implicit operator Gender(string name)
        => FromName(name);

    public static implicit operator Gender(int value)
        => FromValue(value);

    public static implicit operator string(Gender status)
        => status.Name;

    public static implicit operator int(Gender status)
        => status.Value;
}