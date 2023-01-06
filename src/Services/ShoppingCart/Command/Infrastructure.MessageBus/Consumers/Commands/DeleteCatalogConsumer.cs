using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeleteCatalogConsumer : Consumer<Command.DeleteCatalog>
{
    public DeleteCatalogConsumer(IInteractor<Command.DeleteCatalog> interactor)
        : base(interactor) { }
}