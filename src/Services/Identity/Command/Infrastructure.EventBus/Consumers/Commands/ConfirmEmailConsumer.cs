using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class ConfirmEmailConsumer(IInteractor<Command.ConfirmEmail> interactor) : Consumer<Command.ConfirmEmail>(interactor);