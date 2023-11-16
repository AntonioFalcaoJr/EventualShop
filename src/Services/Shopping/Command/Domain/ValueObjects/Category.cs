namespace Domain.ValueObjects;

public record Category
{
    private readonly string _value;

    public Category(string category)
    {
        category = category.Trim();
        ArgumentException.ThrowIfNullOrEmpty(category);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(category.Length, 50);

        _value = category;
    }

    public static explicit operator Category(string category) => new(category);
    public static implicit operator string(Category category) => category._value;
    public override string ToString() => _value;
}