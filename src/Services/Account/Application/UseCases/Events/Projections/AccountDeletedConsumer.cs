using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using AccountDeletedEvent = Messages.Accounts.Events.AccountDeleted;

namespace Application.UseCases.Events.Projections
{
    public class AccountDeletedConsumer : IConsumer<AccountDeletedEvent>
    {
        private readonly IAccountProjectionsService _projectionsService;

        public AccountDeletedConsumer(IAccountProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<AccountDeletedEvent> context)
        {
            await _projectionsService
                .RemoveAsync<AccountDetailsProjection>(
                    projection => projection.Id == context.Message.AccountId,
                    context.CancellationToken);
        }
    }
}