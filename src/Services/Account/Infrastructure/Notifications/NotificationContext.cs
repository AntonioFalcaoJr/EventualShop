using System.Collections.Generic;
using System.Linq;
using Application.Abstractions.Notifications;
using FluentValidation.Results;

namespace Infrastructure.Notifications;

public class NotificationContext : INotificationContext
{
    private readonly List<string> _errors = new();

    public IEnumerable<string> Errors
        => _errors;

    public bool HasErrors
        => Errors.Any();

    public void AddErrors(IEnumerable<ValidationFailure> validationFailures)
        => _errors.AddRange(validationFailures.Select(failure => failure.ErrorMessage));
}