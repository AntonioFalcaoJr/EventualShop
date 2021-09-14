using System.Threading.Tasks;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class CatalogDeletedConsumer : IConsumer<Events.CatalogDeleted>
    {
        public async Task Consume(ConsumeContext<Events.CatalogDeleted> context)
        {
            // var catalog = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            //
            // var catalogDetails = new CatalogProjection
            // {
            //     Id = catalog.Id,
            //     Title = catalog.Title,
            //     
            // };
            //
            // await _projectionsService.ProjectNewAccountDetailsAsync(catalogDetails, context.CancellationToken);
        }
    }
}