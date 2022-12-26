using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Communication;

public static class DomainEvent
{
    public record NotificationRequested(Guid NotificationId, IEnumerable<Dto.NotificationMethod> Methods) : Message, IEvent;

    public record NotificationMethodFailed(Guid NotificationId, Guid MethodId) : Message, IEvent;

    public record NotificationMethodSent(Guid NotificationId, Guid MethodId) : Message, IEvent;

    public record NotificationMethodCancelled(Guid NotificationId, Guid MethodId) : Message, IEvent;

    public record NotificationMethodReset(Guid NotificationId, Guid MethodId) : Message, IEvent;
}