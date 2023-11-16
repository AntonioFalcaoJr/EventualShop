namespace Domain.ValueObjects;

public record Version
{
    private readonly uint _value;

    private Version(string version)
    {
        version = version.Trim();

        if (uint.TryParse(version, out var parsedVersion) is false)
            throw new ArgumentException("Version must be a valid number");

        _value = parsedVersion;
    }

    private Version(uint version)
    {
        _value = version;
    }

    public static Version Zero { get; } = new(0);
    public static Version Initial { get; } = new(1);
    public Version Next => new(_value + 1);
    public static Version Number(uint version) => new(version);
    public static Version Number(string version) => new(version);
    public static Version operator ++(Version version) => new(version._value + 1);
    public static explicit operator Version(string version) => new(version);
    public static explicit operator Version(uint version) => new(version);
    public static implicit operator string(Version version) => version.ToString();
    public static implicit operator uint(Version version) => version._value;
    public static bool operator <(Version left, Version right) => left._value < right._value;
    public static bool operator >(Version left, Version right) => left._value > right._value;
    public static bool operator %(Version left, Version right) => left._value % right._value == 0;
    public static bool operator %(Version left, int right) => left._value % right == 0;
    public static bool operator %(Version left, ulong right) => left._value % right == 0;
    public override string ToString() => _value.ToString();
}