using Contracts.Abstractions.Messages;

namespace Contracts.Services.Communication;

public static class DomainEvent
{
    public record EmailConfirmationRequested(Guid Id, Guid UserId, Guid MethodId, string Email) : Message, IEvent;
}