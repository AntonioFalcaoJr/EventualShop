using System.Threading.Tasks;
using Domain.Entities.Customers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Projections
{
    public class EmailService : IConsumer<Events.CustomerRegistered>
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public EmailService() { }

        public async Task Consume(ConsumeContext<Events.CustomerRegistered> context)
        {
            _logger.LogInformation("Sending email to {Name}", context.Message.Name);
            await Task.CompletedTask;
        }
    }
}