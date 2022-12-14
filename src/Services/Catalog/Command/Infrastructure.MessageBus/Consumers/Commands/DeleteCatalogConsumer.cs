using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Catalog;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DeleteCatalogConsumer : Consumer<Command.DeleteCatalog>
{
    public DeleteCatalogConsumer(IDeleteCatalogInteractor interactor)
        : base(interactor) { }
}