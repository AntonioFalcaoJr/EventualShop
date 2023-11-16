using Application.Abstractions;
using Contracts.Boundaries.Warehouse;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class ReceiveInventoryItemConsumer(IInteractor<Command.ReceiveInventoryItem> interactor) : Consumer<Command.ReceiveInventoryItem>(interactor);