using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class AddCatalogItemConsumer : Consumer<Command.AddCatalogItem>
{
    public AddCatalogItemConsumer(IAddCatalogItemInteractor interactor)
        : base(interactor) { }
}