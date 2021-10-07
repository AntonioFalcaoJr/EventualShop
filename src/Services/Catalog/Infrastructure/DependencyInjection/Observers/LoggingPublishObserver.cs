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
            Log.Information("Starting publishing of {MessageType} event", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public async Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            Log.Information("The event {MessageType} was successfully published", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
            => Task.CompletedTask;
    }
}