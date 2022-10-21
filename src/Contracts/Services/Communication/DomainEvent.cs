using Contracts.Abstractions.Messages;

namespace Contracts.Services.Communication;

public static class DomainEvent
{
    public record EmailConfirmationRequested(Guid Id, Guid MethodId, string Email) : Message, IEvent;
}