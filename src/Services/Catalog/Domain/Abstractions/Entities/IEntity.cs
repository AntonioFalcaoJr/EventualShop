using FluentValidation.Results;

namespace Domain.Abstractions.Entities;

public interface IEntity
{
    Guid Id { get; }
    bool IsDeleted { get; }
    bool IsValid { get; }
    Task<bool> IsValidAsync { get; }
    public IEnumerable<ValidationFailure> Errors { get; }
}