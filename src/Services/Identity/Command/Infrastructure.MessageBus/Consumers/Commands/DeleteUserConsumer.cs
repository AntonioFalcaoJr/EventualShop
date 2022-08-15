using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeleteUserConsumer : Consumer<Command.DeleteUser>
{
    public DeleteUserConsumer(IInteractor<Command.DeleteUser> interactor)
        : base(interactor) { }
}