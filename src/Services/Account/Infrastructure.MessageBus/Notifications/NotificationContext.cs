using System.Collections.Generic;
using System.Linq;
using Application.Abstractions.Notifications;
using FluentValidation.Results;

namespace Infrastructure.MessageBus.Notifications;

public class NotificationContext : INotificationContext
{
    public ValidationResult ValidationResult { get; } = new();

    public IEnumerable<string> Errors
        => ValidationResult.Errors.Select(failure => failure.ErrorMessage);

    public bool HasErrors
        => ValidationResult.IsValid is false;

    public void AddErrors(IEnumerable<ValidationFailure> validationFailures)
        => ValidationResult.Errors.AddRange(validationFailures);
}