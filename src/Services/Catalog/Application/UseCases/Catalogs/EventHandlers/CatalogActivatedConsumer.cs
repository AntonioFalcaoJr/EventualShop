using System.Threading.Tasks;
using Domain.Entities.Catalogs;
using MassTransit;

namespace Application.UseCases.Catalogs.EventHandlers
{
    public class CatalogActivatedConsumer : IConsumer<Events.CatalogActivated>
    {
        public async Task Consume(ConsumeContext<Events.CatalogActivated> context)
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