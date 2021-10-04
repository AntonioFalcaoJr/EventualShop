namespace Application.UseCases.EventHandlers
{
    // public class AccountRegisteredConsumer : IConsumer<Events.ShoppingCartCreated>
    // {
    //     private readonly IShoppingCartEventStoreService _eventStoreService;
    //     private readonly IShoppingCartProjectionsService _projectionsService;
    //
    //     public AccountRegisteredConsumer(IShoppingCartEventStoreService eventStoreService, IShoppingCartProjectionsService projectionsService)
    //     {
    //         _eventStoreService = eventStoreService;
    //         _projectionsService = projectionsService;
    //     }
    //
    //     public async Task Consume(ConsumeContext<Events.ShoppingCartCreated> context)
    //     {
    //         var (aggregateId, _, _) = context.Message;
    //
    //         var account = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);
    //
    //         var accountDetails = new AccountDetailsProjection
    //         {
    //             CatalogId = account.CatalogId,
    //             Age = account.Age,
    //             Name = account.Name
    //         };
    //
    //         await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
    //     }
    // }
}