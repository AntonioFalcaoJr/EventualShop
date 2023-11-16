using Application.Abstractions;
using Contracts.Boundaries.Warehouse;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class DecreaseInventoryAdjustConsumer(IInteractor<Command.DecreaseInventoryAdjust> interactor) : Consumer<Command.DecreaseInventoryAdjust>(interactor);