using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DelayedEvent
{
    public record EmailConfirmationExpired(Guid UserId, string Email) : Message, IEvent;
}