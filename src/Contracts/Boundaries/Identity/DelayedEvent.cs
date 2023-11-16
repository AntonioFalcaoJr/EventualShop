using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Identity;

public static class DelayedEvent
{
    public record EmailConfirmationExpired(Guid UserId, string Email) : Message, IDelayedEvent;
}