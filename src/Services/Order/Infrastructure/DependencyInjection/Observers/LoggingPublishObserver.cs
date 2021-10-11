using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.Observers
{
    public class LoggingPublishObserver : IPublishObserver
    {
        public async Task PrePublish<T>(PublishContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("Starting publishing of {Message} event from {Namespace}", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public async Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("The event {MessageType} from {Namespace} was successfully published", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
            => Task.CompletedTask;
    }
}