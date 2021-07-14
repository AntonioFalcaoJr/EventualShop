using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Abstractions.Entities
{
    public abstract class Entity<TId> : IEntity<TId>
        where TId : struct
    {
        [JsonIgnore]
        private ValidationResult _validationResult = new();

        [JsonIgnore]
        public bool IsValid
            => Validate();

        [JsonIgnore]
        public IReadOnlyCollection<ValidationFailure> Errors
            => _validationResult.Errors;

        [JsonProperty] 
        public TId Id { get; protected set; }

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