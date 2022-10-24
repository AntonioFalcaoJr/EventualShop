using Domain.Abstractions.Entities;
using FluentValidation;

namespace Domain.Abstractions.Validators;

public abstract class EntityValidator<TEntity> : AbstractValidator<TEntity>
    where TEntity : IEntity
{
    protected EntityValidator()
    {
        RuleFor(entity => entity.Id)
            .NotEmpty();
    }
}