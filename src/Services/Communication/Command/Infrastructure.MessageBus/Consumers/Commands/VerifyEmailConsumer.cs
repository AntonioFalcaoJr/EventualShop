using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class VerifyEmailConsumer : Consumer<Command.VerifyEmail>
{
    public VerifyEmailConsumer(IInteractor<Command.VerifyEmail> interactor)
        : base(interactor) { }
}