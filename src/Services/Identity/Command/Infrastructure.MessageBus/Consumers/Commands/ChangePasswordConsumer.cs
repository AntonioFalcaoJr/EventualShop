using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ChangePasswordConsumer : Consumer<Command.ChangePassword>
{
    public ChangePasswordConsumer(IInteractor<Command.ChangePassword> interactor)
        : base(interactor) { }
}