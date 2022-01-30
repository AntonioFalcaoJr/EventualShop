using System.Collections.Generic;
using FluentValidation.Results;

namespace Application.Abstractions.Notifications;

public interface INotificationContext
{
    bool HasErrors { get; }
    IEnumerable<string> Errors { get; }
    ValidationResult ValidationResult { get; }
    void AddErrors(IEnumerable<ValidationFailure> validationFailures);
}