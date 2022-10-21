using Contracts.Abstractions.Messages;

namespace Contracts.Services.Communication;

public static class Command
{
    public record RequestEmailConfirmation(Guid Id, string Email) : Message, ICommand;
}