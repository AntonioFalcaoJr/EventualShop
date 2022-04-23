using Application.Abstractions.Projections;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers;

public class ProjectCatalogItemsWhenChangedConsumer :
    IConsumer<DomainEvents.CatalogDeleted>,
    IConsumer<DomainEvents.CatalogItemAdded>,
    IConsumer<DomainEvents.CatalogItemRemoved>,
    IConsumer<DomainEvents.CatalogItemUpdated>
{
    private readonly IProjectionRepository<Projections.CatalogItem> _projectionRepository;

    public ProjectCatalogItemsWhenChangedConsumer(IProjectionRepository<Projections.CatalogItem> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => await _projectionRepository.DeleteAsync(item => item.CatalogId == context.Message.CatalogId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogItemRemoved> context)
        => await _projectionRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogItemAdded> context)
    {
        Projections.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        await _projectionRepository.InsertAsync(catalogItem, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogItemUpdated> context)
    {
        Projections.CatalogItem catalogItem = new(
            context.Message.CatalogId,
            context.Message.ItemId,
            context.Message.Name,
            context.Message.Description,
            context.Message.Price,
            context.Message.PictureUri,
            false);

        await _projectionRepository.UpsertAsync(catalogItem, context.CancellationToken);
    }
}