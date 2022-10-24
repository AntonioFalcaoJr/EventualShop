using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TValidator> : IEntity
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly TValidator _validator = new();

    [JsonIgnore]
    private ValidationResult _validationResult = new();

    [JsonIgnore]
    private ValidationContext<IEntity> ValidationContext
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

    public Guid Id { get; protected set; }
    public bool IsDeleted { get; protected set; }

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