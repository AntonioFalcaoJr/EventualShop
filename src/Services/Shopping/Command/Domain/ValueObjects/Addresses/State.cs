namespace Domain.ValueObjects.Addresses;

public record State
{
    public string Value { get; }

    public State(string state)
    {
        state = state.Trim();
        ArgumentException.ThrowIfNullOrEmpty(state, nameof(state));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(state.Length, 100, nameof(state));

        Value = state;
    }

    public static implicit operator State(string state) => new(state);
    public static implicit operator string(State state) => state.Value;
}