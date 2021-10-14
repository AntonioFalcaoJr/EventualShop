using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace Infrastructure.DependencyInjection.Observers
{
    public class LoggingSendObserver : ISendObserver
    {
        public async Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("Sending {Message} message from {Namespace}", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public async Task PostSend<T>(SendContext<T> context)
            where T : class
        {
            var messageType = context.Message.GetType();
            Log.Information("{MessageType} message from {Namespace} was sent", messageType.Name, messageType.Namespace);
            await Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
            => Task.CompletedTask;
    }
}