using Contracts.Abstractions.Messages;

namespace Contracts.Services.Communication;

public static class Command
{
    public record RequestEmailConfirmation(Guid Id, string Email) : Message, ICommand; // TODO Este ID deveria ser um ID da Notification, ou do User?
}