namespace Domain.ValueObjects;

public record Brand
{
    private readonly string _name;
    private readonly string _prefix;

    public Brand(string name, string prefix)
    {
        name = name.Trim();
        prefix = prefix.Trim();
        
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        
        ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 30);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(prefix.Length, 5);
      
        _name = name;
        _prefix = prefix;
    }

    public static explicit operator Brand((string name, string prefix) tuple) => new(tuple.name, tuple.prefix);
    public static implicit operator string(Brand brand) => brand._prefix;
    public override string ToString() => _name;
}