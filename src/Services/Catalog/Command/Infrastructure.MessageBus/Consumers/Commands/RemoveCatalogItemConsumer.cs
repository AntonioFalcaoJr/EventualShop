using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RemoveCatalogItemConsumer : Consumer<Command.RemoveCatalogItem>
{
    public RemoveCatalogItemConsumer(IInteractor<Command.RemoveCatalogItem> interactor)
        : base(interactor) { }
}