using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ActivateCatalogConsumer : Consumer<Command.ActivateCatalog>
{
    public ActivateCatalogConsumer(IInteractor<Command.ActivateCatalog> interactor)
        : base(interactor) { }
}