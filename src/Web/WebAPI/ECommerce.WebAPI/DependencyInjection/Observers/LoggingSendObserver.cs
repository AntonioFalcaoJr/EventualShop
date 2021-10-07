using System;
using System.Threading.Tasks;
using MassTransit;
using Serilog;

namespace ECommerce.WebAPI.DependencyInjection.Observers
{
    public class LoggingSendObserver : ISendObserver
    {
        public async Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            Log.Information("Starting sending of {MessageType} command", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public async Task PostSend<T>(SendContext<T> context)
            where T : class
        {
            Log.Information("The command {MessageType} was successfully sent", context.Message.GetType().Name);
            await Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception)
            where T : class
            => Task.CompletedTask;
    }
}