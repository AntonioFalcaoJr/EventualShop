using Contracts.Abstractions.Messages;

namespace Contracts.Services.Communication;

public static class Command
{
    public record RequestEmailConfirmation(Guid UserId, string Email) : Message, ICommand;
}