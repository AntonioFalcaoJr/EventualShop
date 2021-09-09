using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Domain.Abstractions.Entities
{
    public abstract class Entity<TId> : IEntity<TId>
        where TId : struct
    {
        [JsonIgnore]
        private ValidationResult ValidationResult { get; set; } = new();

        [JsonIgnore]
        public bool IsValid
            => Validate();

        [JsonIgnore]
        public IEnumerable<ValidationFailure> Errors
            => ValidationResult.Errors;

        public TId Id { get; protected set; }

        protected bool OnValidate<TValidator, TEntity>()
            where TValidator : AbstractValidator<TEntity>, new()
            where TEntity : Entity<TId>
        {
            ValidationResult = new TValidator().Validate(this as TEntity);
            return ValidationResult.IsValid;
        }

        protected bool OnValidate<TValidator, TEntity>(Func<AbstractValidator<TEntity>, TEntity, ValidationResult> validation)
            where TValidator : AbstractValidator<TEntity>, new()
            where TEntity : Entity<TId>
        {
            ValidationResult = validation(new TValidator(), this as TEntity);
            return ValidationResult.IsValid;
        }

        protected void AddError(string errorMessage, IEnumerable<ValidationFailure> failures)
        {
            AddError(errorMessage);
            ValidationResult.Errors.AddRange(failures);
        }

        protected void AddError(string errorMessage)
            => ValidationResult.Errors.Add(new ValidationFailure(default, errorMessage));

        protected abstract bool Validate();
    }
}