using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Communication;

public static class DomainEvent
{
    public record NotificationRequested(Guid NotificationId, IEnumerable<Dto.NotificationMethod> Methods, long Version) : Message, IDomainEvent;

    public record NotificationMethodFailed(Guid NotificationId, Guid MethodId, long Version) : Message, IDomainEvent;

    public record NotificationMethodSent(Guid NotificationId, Guid MethodId, long Version) : Message, IDomainEvent;

    public record NotificationMethodCancelled(Guid NotificationId, Guid MethodId, long Version) : Message, IDomainEvent;

    public record NotificationMethodReset(Guid NotificationId, Guid MethodId, long Version) : Message, IDomainEvent;
}