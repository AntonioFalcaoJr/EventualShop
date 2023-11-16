using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class ChangePasswordConsumer(IInteractor<Command.ChangePassword> interactor) : Consumer<Command.ChangePassword>(interactor);