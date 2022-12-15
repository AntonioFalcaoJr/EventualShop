using FluentValidation;

namespace Domain.Abstractions.Entities;

public abstract class Entity<TValidator> : IEntity
    where TValidator : IValidator, new()
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected void Validate()
        => new TValidator()
            .Validate(ValidationContext<IEntity>
                .CreateWithOptions(this, strategy
                    => strategy.ThrowOnFailures()));
}