using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CreateCatalogConsumer : Consumer<Command.CreateCatalog>
{
    public CreateCatalogConsumer(IInteractor<Command.CreateCatalog> interactor)
        : base(interactor) { }
}