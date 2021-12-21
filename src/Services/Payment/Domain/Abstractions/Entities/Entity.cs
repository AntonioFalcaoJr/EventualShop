using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TId> : IEntity<TId>
    where TId : struct
{
    [JsonIgnore]
    private ValidationResult _validationResult = new();

    [JsonIgnore]
    public IEnumerable<ValidationFailure> Errors
        => _validationResult.Errors;

    [JsonIgnore]
    public bool IsValid
        => Validate();

    public TId Id { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected bool OnValidate<TValidator, TEntity>()
        where TValidator : AbstractValidator<TEntity>, new()
        where TEntity : Entity<TId>
    {
        _validationResult = new TValidator().Validate(this as TEntity);
        return _validationResult.IsValid;
    }

    protected abstract bool Validate();
}