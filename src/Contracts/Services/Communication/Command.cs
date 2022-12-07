using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Communication;

public static class Command
{
    public record RequestNotification(IEnumerable<Dto.NotificationMethod> Methods) : Message, ICommand;
}