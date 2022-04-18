using Application.EventSourcing.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogDeleted>,
    IConsumer<DomainEvents.CatalogItemAdded>,
    IConsumer<DomainEvents.CatalogItemRemoved>,
    IConsumer<DomainEvents.CatalogItemUpdated>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public ProjectCatalogItemsWhenChangedConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => _projectionsService.RemoveItemsAsync(context.Message.CatalogId, context.CancellationToken);
    
    public Task Consume(ConsumeContext<DomainEvents.CatalogItemRemoved> context)
        => _projectionsService.RemoveItemAsync(context.Message.ItemId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemAdded> context)
        => ProjectAsync(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false,
            context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CatalogItemUpdated> context)
        => ProjectAsync(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false,
            context.CancellationToken);

    private async Task ProjectAsync(Guid catalogId, Guid itemId, string name, string description, decimal price, string pictureUri, bool isDeleted, CancellationToken cancellationToken)
    {
        var catalogItem = new Projections.CatalogItem(catalogId, itemId, name, description, price, pictureUri, isDeleted);
        await _projectionsService.ProjectAsync(catalogItem, cancellationToken);
    }
}