using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Abstractions.Entities
{
    public abstract class Entity<TId> : IEntity
        where TId : struct
    {
        [NotMapped] 
        private ValidationResult _validationResult = new();

        public TId Id { get; protected init; }

        [NotMapped]
        public bool IsValid
            => Validate();

        [NotMapped]
        public IReadOnlyCollection<ValidationFailure> Errors
            => _validationResult.Errors;

        protected bool OnValidate<TValidator, TEntity>(TEntity entity, TValidator validator)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : Entity<TId>
        {
            _validationResult = validator.Validate(entity);
            return _validationResult.IsValid;
        }

        protected bool OnValidate<TValidator, TEntity>(TEntity entity, TValidator validator,
            Func<AbstractValidator<TEntity>, TEntity, ValidationResult> validation)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : Entity<TId>
        {
            _validationResult = validation(validator, entity);
            return _validationResult.IsValid;
        }

        protected void AddError(string errorMessage, ValidationResult validationResult = default)
        {
            _validationResult.Errors.Add(new ValidationFailure(default, errorMessage));
            validationResult?.Errors.ToList().ForEach(failure => _validationResult.Errors.Add(failure));
        }

        protected abstract bool Validate();
    }
}