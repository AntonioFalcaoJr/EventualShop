using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace ECommerce.WebAPI.DependencyInjection.Observers
{
    public class LoggingConsumeObserver : IConsumeObserver
    {
        public async Task PreConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("Starting consumption of {Message} message from {Namespace}", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("The message {Message} from {Namespace} was successfully consumed", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
            where T : class
            => Task.CompletedTask;
    }
}