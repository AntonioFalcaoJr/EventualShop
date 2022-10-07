using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ConfirmEmailConsumer : Consumer<Command.ConfirmEmail>
{
    public ConfirmEmailConsumer(IInteractor<Command.ConfirmEmail> interactor)
        : base(interactor) { }
}