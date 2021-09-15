using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using ECommerce.WebAPI.Messages.Catalogs;
using MassTransit;
using Messages.Catalogs.Queries;
using Messages.Catalogs.Queries.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class CatalogsController : ApplicationController
    {
        public CatalogsController(IBus bus) 
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetCatalog([FromQuery] Queries.GetCatalogItemsDetailsWithPaginationWithPaginationQuery withPaginationWithPaginationQuery, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetCatalogItemsDetailsWithPagination, CatalogItemsDetailsPagedResult>(withPaginationWithPaginationQuery, cancellationToken);

        [HttpPost]
        public Task<IActionResult> CreateCatalog(Commands.CreateCatalogCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPost]
        public Task<IActionResult> AddCatalogItem(Commands.AddCatalogItemCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RemoveCatalogItem(Commands.RemoveCatalogItemCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> ActivateCatalog(Commands.ActivateCatalogCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> DeactivateCatalog(Commands.DeactivateCatalogCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateCatalog(Commands.UpdateCatalogCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
        
        [HttpPut]
        public Task<IActionResult> UpdateCatalogItem(Commands.UpdateCatalogItemCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteCatalog(Commands.DeleteCatalogCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}