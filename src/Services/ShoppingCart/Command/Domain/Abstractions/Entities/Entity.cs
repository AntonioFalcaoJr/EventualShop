using FluentValidation;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TId, TValidator> : IEntity<TId>
    where TId : IIdentifier, new()
    where TValidator : IValidator, new()
{
    public TId Id { get; protected set; } = (TId)Identifier<TId>.Undefined;
    public bool IsDeleted { get; protected set; }

    protected void Validate() => new TValidator()
        .Validate(ValidationContext<IEntity<TId>>
            .CreateWithOptions(this, strategy
                => strategy.ThrowOnFailures()));
}