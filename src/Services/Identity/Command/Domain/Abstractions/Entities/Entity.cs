using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TValidator> : IEntity
    where TValidator : IValidator, new()
{
    [JsonIgnore]
    private readonly TValidator _validator = new();

    public Guid Id { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected void Validate()
        => _validator.Validate(ValidationContext<IEntity>.CreateWithOptions(this, strategy
            => strategy.ThrowOnFailures()));
}