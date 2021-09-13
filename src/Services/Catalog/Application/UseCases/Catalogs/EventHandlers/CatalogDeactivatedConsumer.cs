using System.Threading.Tasks;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.Catalogs.EventHandlers
{
    public class CatalogDeactivatedConsumer : IConsumer<Events.CatalogDeactivated>
    {
        public async Task Consume(ConsumeContext<Events.CatalogDeactivated> context)
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