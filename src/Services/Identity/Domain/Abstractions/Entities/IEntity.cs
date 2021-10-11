using System.Collections.Generic;
using FluentValidation.Results;

namespace Domain.Abstractions.Entities
{
    public interface IEntity<out TId>
        where TId : struct
    {
        TId Id { get; }
        bool IsDeleted { get; }
        bool IsValid { get; }
        public IEnumerable<ValidationFailure> Errors { get; }
    }
}