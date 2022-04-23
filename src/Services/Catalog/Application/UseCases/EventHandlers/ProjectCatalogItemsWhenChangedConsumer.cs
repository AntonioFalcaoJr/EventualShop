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
    private readonly IProjectionsRepository<Projections.CatalogItem> _projectionsRepository;

    public ProjectCatalogItemsWhenChangedConsumer(IProjectionsRepository<Projections.CatalogItem> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogDeleted> context)
        => await _projectionsRepository.DeleteAsync(item => item.CatalogId == context.Message.CatalogId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.CatalogItemRemoved> context)
        => await _projectionsRepository.DeleteAsync(context.Message.ItemId, context.CancellationToken);

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

        await _projectionsRepository.InsertAsync(catalogItem, context.CancellationToken);
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

        await _projectionsRepository.UpsertAsync(catalogItem, context.CancellationToken);
    }
}