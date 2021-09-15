namespace Application.UseCases.EventHandlers
{
    // public class AccountRegisteredConsumer : IConsumer<Events.ShoppingCartCreated>
    // {
    //     private readonly IShoppingCartEventStoreService _eventStoreService;
    //     private readonly IAccountProjectionsService _projectionsService;
    //
    //     public AccountRegisteredConsumer(IShoppingCartEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
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
    //             Id = account.Id,
    //             Age = account.Age,
    //             Name = account.Name
    //         };
    //
    //         await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
    //     }
    // }
}