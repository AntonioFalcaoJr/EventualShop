using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Abstractions.ValueObjects;

public abstract record ValueObject<TValidator>
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly TValidator _validator = new();

    [JsonIgnore]
    private ValidationResult _validationResult = new();

    [JsonIgnore]
    private ValidationContext<ValueObject<TValidator>> ValidationContext
        => new(this);

    [JsonIgnore]
    public IEnumerable<ValidationFailure> Errors
        => _validationResult.Errors;

    [JsonIgnore]
    public bool IsValid
        => Validate();

    [JsonIgnore]
    public Task<bool> IsValidAsync
        => ValidateAsync();

    private bool Validate()
    {
        _validationResult = _validator.Validate(ValidationContext);
        return _validationResult.IsValid;
    }

    private async Task<bool> ValidateAsync()
    {
        _validationResult = await _validator.ValidateAsync(ValidationContext);
        return _validationResult.IsValid;
    }
}