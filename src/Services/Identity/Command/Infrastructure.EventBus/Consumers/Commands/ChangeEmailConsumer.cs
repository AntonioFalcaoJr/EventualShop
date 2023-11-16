using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class ChangeEmailConsumer(IInteractor<Command.ChangeEmail> interactor) : Consumer<Command.ChangeEmail>(interactor);