using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddCatalogItemConsumer : Consumer<Command.AddCatalogItem>
{
    public AddCatalogItemConsumer(IInteractor<Command.AddCatalogItem> interactor)
        : base(interactor) { }
}