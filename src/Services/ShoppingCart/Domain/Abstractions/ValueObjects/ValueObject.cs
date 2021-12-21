using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Domain.Abstractions.ValueObjects;

public abstract record ValueObject
{
    [JsonIgnore]
    private ValidationResult ValidationResult { get; set; } = new();

    [JsonIgnore]
    public bool IsValid
        => Validate();

    [JsonIgnore]
    public IEnumerable<ValidationFailure> Errors
        => ValidationResult.Errors;

    protected bool OnValidate<TValidator, TValueObject>()
        where TValidator : AbstractValidator<TValueObject>, new()
        where TValueObject : ValueObject
    {
        ValidationResult = new TValidator().Validate(this as TValueObject);
        return ValidationResult.IsValid;
    }

    protected abstract bool Validate();
}