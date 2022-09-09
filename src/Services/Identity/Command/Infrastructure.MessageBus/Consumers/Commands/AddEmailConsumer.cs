using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddEmailConsumer : Consumer<Command.AddEmail>
{
    public AddEmailConsumer(IInteractor<Command.AddEmail> interactor)
        : base(interactor) { }
}