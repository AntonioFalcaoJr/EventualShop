using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangeEmailConsumer : Consumer<Command.ChangeEmail>
{
    public ChangeEmailConsumer(IInteractor<Command.ChangeEmail> interactor)
        : base(interactor) { }
}