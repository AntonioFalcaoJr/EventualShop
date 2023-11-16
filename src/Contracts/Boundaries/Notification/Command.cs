using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Notification;

public static class Command
{
    public record RequestNotification(IEnumerable<Dto.NotificationMethod> Methods) : Message, ICommand;

    public record EmitNotificationMethod(Guid NotificationId, Guid MethodId) : Message, ICommand;

    public record FailNotificationMethod(Guid NotificationId, Guid MethodId) : Message, ICommand;

    public record CancelNotificationMethod(Guid NotificationId, Guid MethodId) : Message, ICommand;

    public record SendNotificationMethod(Guid NotificationId, Guid MethodId) : Message, ICommand;

    public record ResetNotificationMethod(Guid NotificationId, Guid MethodId) : Message, ICommand;
}