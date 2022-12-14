using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RemoveCatalogItemConsumer : Consumer<Command.RemoveCatalogItem>
{
    public RemoveCatalogItemConsumer(IRemoveCatalogItemInteractor interactor)
        : base(interactor) { }
}