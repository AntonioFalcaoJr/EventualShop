using System.Collections.Generic;
using System.Linq;
using Application.Abstractions;
using Application.Abstractions.Notifications;
using FluentValidation.Results;

namespace Infrastructure.Notifications;

public class NotificationContext : INotificationContext
{
    private readonly List<string> _notifications = new();

    public IEnumerable<string> Errors
        => _notifications;

    public bool HasNotifications
        => Errors.Any();

    public void AddNotification(string message)
        => _notifications.Add(message);

    public void AddErrors(IEnumerable<ValidationFailure> validationFailures)
        => _notifications.AddRange(validationFailures.Select(failure => failure.ErrorMessage));
}