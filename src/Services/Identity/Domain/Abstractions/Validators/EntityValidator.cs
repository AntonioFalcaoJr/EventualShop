using Domain.Abstractions.Entities;
using FluentValidation;

namespace Domain.Abstractions.Validators
{
    public abstract class EntityValidator<TEntity, TId> : AbstractValidator<TEntity>
        where TEntity : IEntity<TId>
        where TId : struct
    {
        protected EntityValidator()
        {
            RuleFor(entity => entity.Id)
                .NotEqual(default(TId));
        }
    }
}