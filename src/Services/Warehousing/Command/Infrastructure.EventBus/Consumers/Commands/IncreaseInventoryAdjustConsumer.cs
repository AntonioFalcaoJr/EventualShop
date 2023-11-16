using Application.Abstractions;
using Contracts.Boundaries.Warehouse;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Commands;

public class IncreaseInventoryAdjustConsumer(IInteractor<Command.IncreaseInventoryAdjust> interactor) : Consumer<Command.IncreaseInventoryAdjust>(interactor);