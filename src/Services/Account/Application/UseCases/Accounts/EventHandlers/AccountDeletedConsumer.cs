using System.Threading.Tasks;
using Domain.Entities.Accounts;
using MassTransit;

namespace Application.UseCases.Accounts.EventHandlers
{
    public class AccountDeletedConsumer : IConsumer<Events.AccountDeleted>
    {
        public async Task Consume(ConsumeContext<Events.AccountDeleted> context)
        {
            var id = context.Message;
            await Task.CompletedTask;
        }
    }
}