using Domain.Abstractions;

namespace Domain.Aggregates;

public record CustomerId : Identifier<string>
{
    public new static CustomerId Undefined
        => new("Undefined");

    public CustomerId(string value)
        : base(value) { }
    
    public static implicit operator CustomerId(string id)
        => new(id);
}