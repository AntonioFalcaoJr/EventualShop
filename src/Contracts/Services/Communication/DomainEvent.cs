using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Communication;

public static class DomainEvent
{
    public record NotificationRequested(Guid NotificationId, IEnumerable<Dto.NotificationMethod> Methods) : Message, IEvent;
}